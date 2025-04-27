using PhoneNumbers;
using System.Text.RegularExpressions;

namespace Benchmarks.PhoneNumberParsing
{
    public class PhoneNumberParsingServiceEnhanced
    {
        private readonly static PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();

        // Regex to remove extra zero after country code, using named capture groups for clarity
        private static readonly Regex RemoveExtraZeroAfterCountryCode =
            new Regex(@"^(?<prefix>\+)(?<countryCode>\d{1,3})0", RegexOptions.Compiled);

        // Regex to replace "00" with "+" for numbers starting with international dialing code
        private static readonly Regex ReplaceLeading00 = new Regex(@"^00", RegexOptions.Compiled);

        public PhoneNumberParsingServiceEnhanced()
        {
        }

        public ParsedMsisdn NormalizePhoneNumber(string inputNumber, string fallbackCountryCode)
        {
            string originalInput = inputNumber;

            // Step 1: Replace "00" prefix with "+" (for example, "0044" becomes "+44")
            if (inputNumber.StartsWith("00"))
            {
                inputNumber = ReplaceLeading00.Replace(inputNumber, "+");
            }

            // Step 2: Remove the extra zero after the country code (for example, "+440" becomes "+44")
            inputNumber = RemoveExtraZeroAfterCountryCode.Replace(inputNumber, "${prefix}${countryCode}");

            PhoneNumber number;
            try
            {
                // Try to parse the number using the provided fallback country code for local numbers
                number = _phoneNumberUtil.Parse(inputNumber, fallbackCountryCode);
            }
            catch (NumberParseException)
            {
                // If parsing fails, return invalid result
                return new ParsedMsisdn
                {
                    Msisdn = originalInput,
                    NormalizedMsisdn = null,
                    IsValid = false,
                    IsLandline = false,
                    CountryCode = null
                };
            }

            // Step 3: Check if the number is valid
            bool isValid = _phoneNumberUtil.IsValidNumber(number);

            // Step 4: Use IsPossibleNumber to ensure the input string is valid
            bool isPossible = _phoneNumberUtil.IsPossibleNumber(number);
            if (!isPossible)
            {
                // If it's not a possible number, we can flag it as invalid
                return new ParsedMsisdn
                {
                    Msisdn = originalInput,
                    NormalizedMsisdn = null,
                    IsValid = false,
                    IsLandline = false,
                    CountryCode = null
                };
            }

            // Step 5: Determine if the number is a landline or mobile
            PhoneNumberType numberType = _phoneNumberUtil.GetNumberType(number);
            bool isLandline = numberType == PhoneNumberType.FIXED_LINE || numberType == PhoneNumberType.FIXED_LINE_OR_MOBILE;

            // Step 6: Get the country code (ISO 2 code) of the phone number
            string regionCode = _phoneNumberUtil.GetRegionCodeForNumber(number);

            // Step 7: Format the phone number in E164 format
            string normalizedMsisdn = _phoneNumberUtil.Format(number, PhoneNumberFormat.E164);

            // Step 8: Strictly compare the original input and the normalized number
            // The key part here is to ensure that if the input is more than just the phone number, it gets flagged.
            bool inputMatchesNormalized = ArePhoneNumbersEquivalent(originalInput, normalizedMsisdn, regionCode);

            // If it's not a close match, and the number is valid, we mark it invalid
            if (!inputMatchesNormalized && isValid)
            {
                isValid = false;
            }

            return new ParsedMsisdn
            {
                Msisdn = originalInput,
                NormalizedMsisdn = normalizedMsisdn,
                IsValid = isValid,
                IsLandline = isLandline,
                CountryCode = regionCode
            };
        }

        private static bool ArePhoneNumbersEquivalent(string input, string normalized, string regionCode)
        {
            // Handle inputs starting with '00' (like '0044') or with a '+' (like '+44')
            string inputWithCountryCode = input.Trim();

            // Case where the input has leading 0 for local numbers (like '044...') -> remove '0' and check
            if (inputWithCountryCode.StartsWith("0") && normalized.StartsWith("+"))
            {
                string countryCode = "+" + _phoneNumberUtil.GetCountryCodeForRegion(regionCode);
                if (normalized.StartsWith(countryCode))
                {
                    string localNumber = normalized.Substring(countryCode.Length);  // Strip the country code
                    string inputLocalNumber = inputWithCountryCode.Substring(1);  // Strip the leading '0'
                    return localNumber.Equals(inputLocalNumber);  // Compare the rest of the phone number
                }
            }

            // Case where the input has '00' or '+' prefix
            if (inputWithCountryCode.StartsWith("00"))
            {
                inputWithCountryCode = "+" + inputWithCountryCode.Substring(2);  // Remove the '00'
            }

            // Compare the whole input with the normalized number
            return normalized.Equals(inputWithCountryCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}

using PhoneNumbers;

namespace Benchmarks.PhoneNumberParsing
{
    internal class PhoneNumberParsingService
    {
        private readonly PhoneNumberUtil _phoneNumberUtil;

        public PhoneNumberParsingService()
        {
            _phoneNumberUtil = PhoneNumberUtil.GetInstance();
        }


        public ParsedMsisdn GetParsedMsisdn(string msisdn)
        {
            var phoneNumber = _phoneNumberUtil.ParseAndKeepRawInput(msisdn, null);
            var normalizedMsisdn = _phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
            var regionCode = _phoneNumberUtil.GetRegionCodeForNumber(phoneNumber);
            var isValid = _phoneNumberUtil.IsValidNumber(phoneNumber);

            return new ParsedMsisdn()
            {
                Msisdn = msisdn,
                NormalizedMsisdn = normalizedMsisdn,
                CountryIso2 = regionCode,
                IsValid = isValid
            };
        }
    }
}

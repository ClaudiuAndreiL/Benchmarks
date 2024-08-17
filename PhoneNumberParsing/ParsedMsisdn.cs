namespace Benchmarks.PhoneNumberParsing
{
    public class ParsedMsisdn
    {
        public string Msisdn { get; set; } = default!;
        public string NormalizedMsisdn { get; set; } = default!;
        public string CountryIso2 { get; set; } = default!;
        public bool IsValid { get; set; }

    }
}

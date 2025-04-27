namespace Benchmarks.DeniedContent.RegexVersion.Models
{
    public class DeniedSenderValidationResult
    {
        public string MatchingDeniedSender { get; set; } = string.Empty;
        public bool IsMatch { get; set; }
    }
}

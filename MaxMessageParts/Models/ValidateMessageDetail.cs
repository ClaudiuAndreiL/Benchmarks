namespace Benchmarks.MaxMessageParts.Models
{
    public class ValidateMessageDetail
    {
        public Guid Id { get; set; }
        public string Destination { get; set; } = string.Empty;
        public int MessagePartCount { get; set; }
    }
}

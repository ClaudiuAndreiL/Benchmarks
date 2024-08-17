namespace Benchmarks.SerializationBenchmarks.Models
{
    public class Request
    {
        public string TenantCode { get; set; } = default!;
        public string AccountReference { get; set; } = default!;
        public string Channel { get; set; } = default!;
        public List<Recipient> Recipients { get; set; } = default!;
    }
}

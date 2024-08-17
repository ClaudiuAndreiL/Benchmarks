namespace Benchmarks.SerializationBenchmarks.Models
{
    public class Recipient
    {
        public string Msisdn { get; set; } = default!;
        public string TextToSend { get; set; } = default!;
        public Dictionary<string, string> Variables { get; set; } = default!;
    }
}

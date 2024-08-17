using BenchmarkDotNet.Attributes;
using Benchmarks.SerializationBenchmarks.Helpers;
using Benchmarks.SerializationBenchmarks.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Benchmarks.SerializationBenchmarks
{
    [BenchmarkCategory("Deserialization")]
    [MemoryDiagnoser]
    //[SkewnessColumn, KurtosisColumn, BaselineColumn, AllStatisticsColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class DeserializationBenchmarks
    {
        [Params(1, 100, 10000, 100000, 500000)]
        public int N;

        private Request request = new();
        private byte[] serializedBytes = default!;

        private string serializedString = default!;

        [GlobalSetup]
        public void Setup()
        {
            request = RequestDataGenerator.GetRequest(N);
            serializedString = JsonConvert.SerializeObject(request);
            serializedBytes = Encoding.UTF8.GetBytes(serializedString);
        }

        [Benchmark(Baseline = true)]
        public void DeserializeNewtonSoft()
        {
            var deserialized = JsonConvert.DeserializeObject<Request>(serializedString!);
        }

        [Benchmark]
        public void DeserializeSystemText()
        {
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<Request>(serializedString!);
        }

        [Benchmark]
        public void DeserializeNewtonSoftStreamed()
        {
            using var memoryStream = new MemoryStream(serializedBytes);
            using var streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            using var jsonReader = new JsonTextReader(streamReader);

            var serializer = new Newtonsoft.Json.JsonSerializer();
            var deserialized = serializer.Deserialize<Request>(jsonReader);
        }

        [Benchmark]
        public void DeserializeSystemTextJsonStreamed()
        {
            using var memoryStream = new MemoryStream(serializedBytes);
            var utf8JsonReader = new Utf8JsonReader(serializedBytes);

            var deserialized = System.Text.Json.JsonSerializer.Deserialize<Request>(ref utf8JsonReader);
        }

        [Benchmark]
        public async Task DeserializeSystemTextJsonStreamedAsync()
        {
            using var memoryStream = new MemoryStream(serializedBytes);
            var deserialized = await System.Text.Json.JsonSerializer.DeserializeAsync<Request>(memoryStream);
        }

    }
}
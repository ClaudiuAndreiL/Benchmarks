using BenchmarkDotNet.Attributes;
using Benchmarks.SerializationBenchmarks.Helpers;
using Benchmarks.SerializationBenchmarks.Models;
using Newtonsoft.Json;

namespace Benchmarks.SerializationBenchmarks
{

    [BenchmarkCategory("SerializeDeserialize")]
    [MemoryDiagnoser]
    [SkewnessColumn, KurtosisColumn, BaselineColumn, AllStatisticsColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class SerializeDeserializeBenchmark
    {
        public SerializeDeserializeBenchmark()
        {
        }

        [Params(1, 10, 100, 1000, 10000, 50000, 100000, 200000)]
        public int N;

        private Request request = new();

        private string? globalSerialized;

        [GlobalSetup]
        public void Setup()
        {
            request = RequestDataGenerator.GetRequest(N);
            globalSerialized = JsonConvert.SerializeObject(request);
        }

        [Benchmark(Baseline = true)]
        public void SerializeDeserializeNewtonSoft()
        {
            var serializedString = JsonConvert.SerializeObject(request);
            var serialized = JsonConvert.DeserializeObject<Request>(serializedString!);
        }

        [Benchmark]
        public void SerializeDeserializeSystemText()
        {
            var serializedString = System.Text.Json.JsonSerializer.Serialize(request);
            var serialized = System.Text.Json.JsonSerializer.Deserialize<Request>(serializedString!);
        }


        [Benchmark]
        public void SerializeNewtonSoft()
        {
            var serialized = JsonConvert.SerializeObject(request);
        }

        [Benchmark]
        public void SerializeSystemText()
        {
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
        }

        [Benchmark]
        public void DeserializeNewtonSoft()
        {
            var serialized = JsonConvert.DeserializeObject<Request>(globalSerialized!);
        }

        [Benchmark]
        public void DeserializeSystemText()
        {
            var serialized = System.Text.Json.JsonSerializer.Deserialize<Request>(globalSerialized!);
        }

    }
}
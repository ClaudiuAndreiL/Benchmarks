using BenchmarkDotNet.Attributes;
using Benchmarks.SerializationBenchmarks.Helpers;
using Benchmarks.SerializationBenchmarks.Models;
using Newtonsoft.Json;

namespace Benchmarks.SerializationBenchmarks
{
    [BenchmarkCategory("Serialization")]
    [MemoryDiagnoser]
    [SkewnessColumn, KurtosisColumn, BaselineColumn, AllStatisticsColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class SerializationBenchmark
    {
        public SerializationBenchmark()
        {
        }

        [Params(1, 10, 100, 1000, 10000, 50000, 100000, 200000)]
        public int N;

        private Request request = new();

        [GlobalSetup]
        public void Setup()
        {
            request = RequestDataGenerator.GetRequest(N);
        }

        [Benchmark(Baseline = true)]
        public void SerializeNewtonSoft()
        {
            var serialized = JsonConvert.SerializeObject(request);
        }

        [Benchmark]
        public void SerializeSystemText()
        {
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
        }

    }
}

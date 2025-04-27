using BenchmarkDotNet.Attributes;
using Benchmarks.DeniedContent.GraphVersion;
using Benchmarks.DeniedContent.GraphVersion2;
using Benchmarks.DeniedContent.RegexVersion;
using Bogus;

namespace Benchmarks.DeniedContent
{
    [BenchmarkCategory("PhoneNumberParsing")]
    [MemoryDiagnoser]
    [SkewnessColumn, KurtosisColumn, BaselineColumn, AllStatisticsColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class DeniedSenderMatchingBenchmark
    {
        private readonly RegexGenerator _regexGenerator;
        private readonly GraphGenerator _graphGenerator;
        private readonly GraphGenerator2 _graphGenerator2;

        public DeniedSenderMatchingBenchmark()
        {
            _regexGenerator = new();
            _graphGenerator = new();
            _graphGenerator2 = new();
        }

        [Params(100, 1000, 2000, 5000, 10000)]
        public int N;

        private List<string>? _additionalSenders;

        [GlobalSetup]
        public void Setup()
        {
            var faker = new Faker();
            var _additionalSenders = Enumerable.Range(0, N)
                .Select(i => faker.Random.Word().Split(' ')[0] + i)
                .ToList();
            _regexGenerator.Generate(_additionalSenders);
            _graphGenerator.Generate(_additionalSenders);
            _graphGenerator2.Generate(_additionalSenders);

        }
        public const string SearchSender = "trop1calGud";

        [Benchmark]
        public void RegexDeniedSenderMatch()
        {
            var results = _regexGenerator.Validate(SearchSender);
        }

        [Benchmark]
        public void GraphDeniedSenderMatch()
        {
            var results = _graphGenerator.Search(SearchSender);
        }

        [Benchmark]
        public void GraphRefactoredDeniedSenderMatch()
        {
            var results = _graphGenerator2.Search(SearchSender);
        }
    }
}

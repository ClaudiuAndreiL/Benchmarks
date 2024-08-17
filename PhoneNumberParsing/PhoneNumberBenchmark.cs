using BenchmarkDotNet.Attributes;

namespace Benchmarks.PhoneNumberParsing
{
    [BenchmarkCategory("PhoneNumberParsing")]
    [MemoryDiagnoser]
    //[SkewnessColumn, KurtosisColumn, BaselineColumn, AllStatisticsColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class PhoneNumberBenchmark
    {
        private readonly PhoneNumberParsingService _phoneNumberParsing;
        public PhoneNumberBenchmark()
        {
            _phoneNumberParsing = new PhoneNumberParsingService();
        }

        [Params(1, 10, 100, 1000, 10000, 100000, 200000, 500000, 1000000)]
        public int N;

        private List<string>? _phoneNumbers;
        private readonly Random _rand = new Random();

        [GlobalSetup]
        public void Setup()
        {
            _phoneNumbers = Enumerable.Range(0, N)
                .Select(i => $"+4072" + _rand.Next(10000000, 100000000))
                .ToList();
        }

        [Benchmark]
        public void ParseMsisdns()
        {
            var results = _phoneNumbers!.Select(x => _phoneNumberParsing.GetParsedMsisdn(x)).ToList();
        }

    }
}
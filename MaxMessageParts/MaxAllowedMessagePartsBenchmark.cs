using BenchmarkDotNet.Attributes;
using Benchmarks.MaxMessageParts.Enums;
using Benchmarks.MaxMessageParts.Models;
using System.Runtime.InteropServices;

namespace Benchmarks.MaxMessageParts
{
    [BenchmarkCategory("MaxMessageParts")]
    [MemoryDiagnoser]
    public class MaxAllowedMessagePartsBenchmark
    {
        public MaxAllowedMessagePartsBenchmark()
        {
        }

        private const int AccountMaxAllowedMessageParts = 50;

        private List<ValidateMessageDetail> Messages = new();

        private readonly Random rand = new Random();

        [Params(1, 10, 100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            Messages = Enumerable.Range(0, N)
                .Select(_ => new ValidateMessageDetail
                {
                    Id = Guid.NewGuid(),
                    Destination = "",
                    MessagePartCount = rand.Next(1, 100)
                })
                .ToList();
        }

        [Benchmark]
        public void SimpleDoStuffWithForI()
        {
            var result = new ValidationHandlerResult { MessageStatus = MessageStatusEnum.MessagePartCountExceeded };
            for (int i = 0; i < Messages.Count; i++)
            {
                if (Messages[i].MessagePartCount <= AccountMaxAllowedMessageParts)
                    continue;

                result.FailedValidationMessageIds.Add(Messages[i].Id);
            }
        }


        [Benchmark]
        public void SimpleDoStuffWithForeach()
        {
            var result = new ValidationHandlerResult { MessageStatus = MessageStatusEnum.MessagePartCountExceeded };
            foreach (var message in Messages)
            {
                if (message.MessagePartCount <= AccountMaxAllowedMessageParts)
                    continue;

                result.FailedValidationMessageIds.Add(message.Id);
            }
        }

        [Benchmark(Baseline = true)]
        public void SimpleDoStuffWithLinq()
        {
            var result = new ValidationHandlerResult { MessageStatus = MessageStatusEnum.MessagePartCountExceeded };
            result.FailedValidationMessageIds = Messages
                .Where(x => x.MessagePartCount > AccountMaxAllowedMessageParts)
                .Select(x => x.Id)
                .ToHashSet();
        }

        [Benchmark]
        public void Span()
        {
            var result = new ValidationHandlerResult { MessageStatus = MessageStatusEnum.MessagePartCountExceeded };
            var span = CollectionsMarshal.AsSpan(Messages);

            foreach (ref ValidateMessageDetail message in span)
            {
                if (message.MessagePartCount > AccountMaxAllowedMessageParts)
                    result.FailedValidationMessageIds.Add(message.Id);
            }
        }

    }
}

using Benchmarks.SerializationBenchmarks.Models;
using Bogus;

namespace Benchmarks.SerializationBenchmarks.Helpers
{
    public static class RequestDataGenerator
    {
        private static readonly Faker faker = new();

        public static Request GetRequest(int noOfRecipients)
        {
            var request = new Request();
            request.TenantCode = "esendex-uk";
            request.AccountReference = "EXUK123131";
            request.Channel = "SMS";

            request.Recipients = Enumerable.Range(0, noOfRecipients)
                .Select(x => new Recipient
                {
                    Msisdn = faker.Person.Phone,
                    TextToSend = new string(faker.Lorem.Sentence(50)),
                    Variables = Enumerable.Range(0, faker.Random.Int(2, 5))
                        .Select(x => Guid.NewGuid().ToString())
                        .ToDictionary(x => x, ValidationException => faker.Random.Word())
                })
                .ToList();

            return request;
        }
    }
}

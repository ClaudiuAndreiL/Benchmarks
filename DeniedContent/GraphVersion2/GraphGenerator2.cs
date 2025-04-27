using Benchmarks.DeniedContent.RegexVersion.Models;
using Benchmarks.DeniedContent.RegexVersion;
using Newtonsoft.Json;

namespace Benchmarks.DeniedContent.GraphVersion2
{
    public class GraphGenerator2
    {
        private readonly ExperimentalGraph2Service _graphService = new();

        public DeniedSenderValidationResult Search(string sender)
        {
            var found = _graphService.Search(sender);

            return string.IsNullOrEmpty(found) ?
                new DeniedSenderValidationResult { IsMatch = false } :
                new DeniedSenderValidationResult { IsMatch = true, MatchingDeniedSender = found };
        }

        public void Generate(List<string>? additionalSenders = null)
        {
            var senders = new List<string>();

            var defaultDeniedSenders = JsonConvert.DeserializeObject<List<string>>(RegexVersionConstants.BaseDeniedSendersStr);

            if (additionalSenders is null)
                senders = defaultDeniedSenders;
            if (additionalSenders is not null)
                senders = defaultDeniedSenders!.Concat(additionalSenders).Distinct().ToList();

            for (int i = 0; i < senders!.Count; i++)
            {
                _graphService.Insert(senders[i]);
            }

        }
    }
}

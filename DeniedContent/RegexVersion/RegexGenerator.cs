using Benchmarks.DeniedContent.RegexVersion.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Benchmarks.DeniedContent.RegexVersion
{
    public class RegexGenerator
    {
        public DestinationValidationData destinationValidationData;
        public RegexGenerator()
        {
        }

        public DeniedSenderValidationResult Validate(string candidateSender)
        {
            var matches = destinationValidationData.Regex.Match(candidateSender);
            if (!matches.Success || destinationValidationData.DeniedSenders.Count == 0)
            {
                return new DeniedSenderValidationResult { IsMatch = false };
            }

            // TODO: can be optimized by caching the encoded words, not redoing the encoding each time (not UC1 needed)
            var foundDenyWord = destinationValidationData.DeniedSenders.FirstOrDefault(word => matches.Groups[Encode(word)].Success);
            if (!string.IsNullOrEmpty(foundDenyWord))
            {
                return new DeniedSenderValidationResult { IsMatch = true, MatchingDeniedSender = foundDenyWord };
            }

            return new DeniedSenderValidationResult { IsMatch = true };
        }

        public void Generate(List<string>? additionalSenders = null)
        {
            var senders = new List<string>();

            var charSubst = JsonConvert.DeserializeObject<Dictionary<char, string>>(RegexVersionConstants.CharReplacementsStr);
            var defaultDeniedSenders = JsonConvert.DeserializeObject<List<string>>(RegexVersionConstants.BaseDeniedSendersStr);
            
            if(additionalSenders is null)
                senders = defaultDeniedSenders;
            if(additionalSenders is not null)
                senders = defaultDeniedSenders!.Concat(additionalSenders).Distinct().ToList();

            var patterns = senders!.Select(x => GetSenderPattern(x, charSubst!)).ToList();
            var pattern = string.Join("|", patterns);

            var regexOptions = false
            ? RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled
            : RegexOptions.IgnoreCase | RegexOptions.Multiline;
            var regex = new Regex(pattern, regexOptions, TimeSpan.FromMilliseconds(100));

            regex.Match("dummy");

            destinationValidationData = new DestinationValidationData
            {
                Regex = regex,
                DeniedSenders = senders!.ToHashSet(),
            };
        }

        private string GetSenderPattern(string sender, Dictionary<char, string> characterReplacements)
        {
            var word = sender.ToLower().ToCharArray();
            var regexGroupName = Encode(sender.ToLower());

            var useReplacements = word.Length >= 3;

            var sb = new StringBuilder($"(?<{regexGroupName}>.*");

            for (var i = 0; i < word.Length; i++)
            {
                var ch = word[i];
                var escapedChar = useReplacements && characterReplacements.TryGetValue(ch, out var replacementPattern)
                    ? replacementPattern
                    : Regex.Escape(ch.ToString());

                sb.Append(escapedChar);

                if (i < word.Length - 1)
                {
                    sb.Append(RegexVersionConstants.SpacerPattern);
                }
            }

            sb.Append(".*)");

            return sb.ToString();
        }

        private static string Encode(string value)
        {
            // Regex group names must be valid C# identifiers ([a-zA-Z0-9_]), so it is needed to be encoded
            // The b64 encoding has + and / as part of the alphabet, so they need to be replaced. Prefix is needed as they might start with a number
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            var encoded = base64.Replace("/", "_a_").Replace("+", "_b_").Replace("=", "");
            return RegexVersionConstants.GroupPrefix + encoded;
        }
    }
}

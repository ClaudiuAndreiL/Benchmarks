using Benchmarks.DeniedContent.GraphVersion;
using Benchmarks.DeniedContent.GraphVersion2.Models;
using System;

namespace Benchmarks.DeniedContent.GraphVersion2
{
    public class ExperimentalGraph2Service
    {
        private GraphNode2 _root = new();

        public bool IsValidSender(string sender)
        {
            return sender.All(c => ExperimentalConstants.Substitutions.ContainsKey(c.ToString()) || ExperimentalConstants.EmptyCharacters.Contains(c));
        }

        public void Reset()
        {
            _root = new();
        }

        public void Insert(string sender, DeniedMatchTypeEnum matchType = DeniedMatchTypeEnum.ContainsMatch)
        {
            var currentNode = _root;

            for (int i = 0; i < sender.Length; i++)
            {
                if (ExperimentalConstants.EmptyCharacters.Contains(sender[i]))
                    continue;

                if (!ExperimentalConstants.SingleCharToStringDict.TryGetValue(sender[i], out var currentCharStr))
                    continue;

                var twoCharSequence = GetTwoCharSequence(sender, i);
                if (twoCharSequence is not null)
                    i++;
                currentCharStr = twoCharSequence ?? currentCharStr;

                if (!ExperimentalConstants.Substitutions.TryGetValue(currentCharStr, out var variants))
                    continue;

                if (!currentNode!.Nodes.TryGetValue(currentCharStr, out var child))
                    child = new GraphNode2();

                foreach (var variant in variants)
                    if(!currentNode.Nodes.ContainsKey(variant))
                        currentNode.Nodes[variant] = child;

                currentNode = child;
            }

            currentNode.DeniedSender = sender;
            currentNode.MatchTypeEnum = matchType;
        }

        public List<string> GetAllItems()
        {
            List<string> results = new();
            Traverse(_root, results);
            return results;
        }

        public string? Search(string sender)
        {
            return SearchRecursive(sender, 0, _root);
        }

        private string? SearchRecursive(string sender, int index, GraphNode2 currentNode)
        {
            if (index >= sender.Length)
                return currentNode.DeniedSender;

            if (ExperimentalConstants.EmptyCharacters.Contains(sender[index]))
                return SearchRecursive(sender, index + 1, currentNode);

            if (!ExperimentalConstants.SingleCharToStringDict.TryGetValue(sender[index], out var one))
                return SearchRecursive(sender, index + 1, currentNode);

            if (index + 1 < sender.Length)
            {
                var two = GetTwoCharSequence(sender, index);
                if(two != null && currentNode.Nodes.TryGetValue(two, out var node2))
                {
                    var candidateFound = EvaluateDeniedSender(sender, node2);
                    if (!string.IsNullOrEmpty(candidateFound))
                        return node2.DeniedSender;

                    var result = SearchRecursive(sender, index + 2, node2);
                    if (result != null)
                        return result;
                }
            }

            if (currentNode.Nodes.TryGetValue(one, out var node1))
            {
                var candidateFound = EvaluateDeniedSender(sender, node1);
                if (!string.IsNullOrEmpty(candidateFound))
                    return node1.DeniedSender;

                var result = SearchRecursive(sender, index + 1, node1);
                if (result != null)
                    return result;
            }

            return SearchRecursive(sender, index + 1, _root);
        }

        private static readonly Dictionary<ushort, string> _twoCharCache =
             ExperimentalConstants.MultiCharSubstitutions.Keys
                 .ToDictionary(
                     k => (ushort)(k[0] << 8 | k[1]),
                     k => k);

        private static string? GetTwoCharSequence(string sender, int i)
        {
            if (i + 1 >= sender.Length)
                return null;
            ushort code = (ushort)(sender[i] << 8 | sender[i + 1]);
            return _twoCharCache.TryGetValue(code, out var seq) ? seq : null;
        }

        private static string? EvaluateDeniedSender(string sender, GraphNode2 candidateNode)
        {
            if (candidateNode.DeniedSender is null)
                return null;

            if (candidateNode.MatchTypeEnum == DeniedMatchTypeEnum.ContainsMatch)
                return candidateNode.DeniedSender;

            if (candidateNode.MatchTypeEnum == DeniedMatchTypeEnum.ExactMatch)
                return sender.Equals(candidateNode.DeniedSender) ? candidateNode.DeniedSender : null;

            if (candidateNode.MatchTypeEnum == DeniedMatchTypeEnum.FuzzyMatch)
                return sender.Length == candidateNode.DeniedSender!.Length ? candidateNode.DeniedSender : null;

            return null;
        }


        private static void Traverse(GraphNode2 node, List<string> results)
        {
            if (node.DeniedSender != null)
            {
                results.Add(node.DeniedSender);
            }

            foreach (var child in node.Nodes.Values)
            {
                Traverse(child, results);
            }
        }
    }
}

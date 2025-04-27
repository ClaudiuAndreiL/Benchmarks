namespace Benchmarks.DeniedContent.GraphVersion.Models
{
    public class GraphNode
    {
        public string? DeniedSender { get; set; }
        public DeniedMatchTypeEnum? MatchTypeEnum { get; set; }

        public Dictionary<HashSet<string>, GraphNode> Nodes { get; set; } = new();
    }
}

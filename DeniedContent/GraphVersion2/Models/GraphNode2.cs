using Benchmarks.DeniedContent.GraphVersion;

namespace Benchmarks.DeniedContent.GraphVersion2.Models
{
    public class GraphNode2
    {
        public string? DeniedSender;
        public DeniedMatchTypeEnum MatchTypeEnum;
        public Dictionary<string, GraphNode2> Nodes = new();
    }
}

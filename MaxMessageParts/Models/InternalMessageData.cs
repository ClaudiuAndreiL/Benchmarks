using Benchmarks.MaxMessageParts.Enums;

namespace Benchmarks.MaxMessageParts.Models
{
    public class InternalMessageData
    {
        public Guid Id { get; set; }
        public MessageStatusEnum Reason { get; set; }
    }
}

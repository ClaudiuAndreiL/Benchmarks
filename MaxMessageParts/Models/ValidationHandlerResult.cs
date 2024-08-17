using Benchmarks.MaxMessageParts.Enums;

namespace Benchmarks.MaxMessageParts.Models
{
    public class ValidationHandlerResult
    {
        public bool ShouldSchedule { get; set; }
        public MessageStatusEnum MessageStatus { get; set; }
        public HashSet<Guid> FailedValidationMessageIds { get; set; } = new();
    }
}

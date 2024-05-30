using System.Collections.Generic;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public class DomainEventPrimitive
    {
        public string Id { get; set; } = string.Empty;
        public string AggregateId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OccurredOn { get; set; } = string.Empty;
        public Dictionary<string, string> Body { get; set; } = new Dictionary<string, string>();
    }
}

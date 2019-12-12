namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using Domain.Bus.Event;

    public class DomainEventJsonUnserializer : IDomainEventUnserializer
    {
        public DomainEvent Unserialize(string domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
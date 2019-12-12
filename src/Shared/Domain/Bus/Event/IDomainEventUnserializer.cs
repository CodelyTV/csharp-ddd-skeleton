namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface IDomainEventUnserializer
    {
        DomainEvent Unserialize(string domainEvent);
    }
}
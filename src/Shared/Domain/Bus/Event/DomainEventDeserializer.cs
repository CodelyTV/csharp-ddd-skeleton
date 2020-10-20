namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface DomainEventDeserializer
    {
        DomainEvent Deserialize(string domainEvent);
    }
}

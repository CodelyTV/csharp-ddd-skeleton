namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface IDomainEventDeserializer
    {
        DomainEvent Deserialize(string domainEvent);
    }
}
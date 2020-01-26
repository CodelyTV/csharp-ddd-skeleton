namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System.Threading.Tasks;

    public interface IDomainEventSubscriberBase
    {
        Task On(DomainEvent @event);
    }
}
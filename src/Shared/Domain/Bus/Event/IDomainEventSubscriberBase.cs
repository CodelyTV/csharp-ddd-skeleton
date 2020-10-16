using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface IDomainEventSubscriberBase
    {
        Task On(DomainEvent @event);
    }
}

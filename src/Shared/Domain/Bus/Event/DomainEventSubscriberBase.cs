using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface DomainEventSubscriberBase
    {
        Task On(DomainEvent @event);
    }
}

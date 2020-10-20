using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface DomainEventSubscriber<TDomain> : DomainEventSubscriberBase where TDomain : DomainEvent
    {
        async Task DomainEventSubscriberBase.On(DomainEvent @event)
        {
            var msg = @event as TDomain;
            if (msg != null)
                await On(msg);
        }

        Task On(TDomain domainEvent);
    }
}

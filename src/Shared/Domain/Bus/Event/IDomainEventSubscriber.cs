using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface IDomainEventSubscriber<TDomain> : IDomainEventSubscriberBase where TDomain : DomainEvent
    {
        async Task IDomainEventSubscriberBase.On(DomainEvent @event)
        {
            var msg = @event as TDomain;
            if (msg != null)
                await On(msg);
        }

        Task On(TDomain domainEvent);
    }
}

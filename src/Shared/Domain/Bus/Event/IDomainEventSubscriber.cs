namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System.Threading.Tasks;

    public interface IDomainEventSubscriber<TDomain> : IDomainEventSubscriberBase where TDomain : DomainEvent
    {
        Task On(TDomain DomainEvent);

        async Task IDomainEventSubscriberBase.On(DomainEvent @event)
        {
            var msg = @event as TDomain;
            if (msg != null)
                await On(msg);
        }
    }
}
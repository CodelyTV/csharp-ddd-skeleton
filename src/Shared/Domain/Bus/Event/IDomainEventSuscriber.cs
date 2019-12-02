namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System.Threading.Tasks;

    public interface IDomainEventSuscriber<TDomain> where TDomain : IDomainEvent
    {
        Task On(TDomain @event);
    }
}
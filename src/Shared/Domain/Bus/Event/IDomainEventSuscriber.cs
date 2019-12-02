namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System.Threading.Tasks;

    public interface IDomainEventSuscriber<TDomain> where TDomain : DomainEvent
    {
        Task On(TDomain @event);
    }
}
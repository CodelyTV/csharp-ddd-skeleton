namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventBus
    {
        Task Publish(List<IDomainEvent> events);
    }
}
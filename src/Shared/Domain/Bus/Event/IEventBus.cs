using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface IEventBus
    {
        Task Publish(List<DomainEvent> events);
    }
}

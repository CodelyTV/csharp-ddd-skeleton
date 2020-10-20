using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface EventBus
    {
        Task Publish(List<DomainEvent> events);
    }
}

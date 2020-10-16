using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public interface IDomainEventsConsumer
    {
        Task Consume();
    }
}

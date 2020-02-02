namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System.Threading.Tasks;

    public interface IDomainEventsConsumer
    {
        Task Consume();
    }
}
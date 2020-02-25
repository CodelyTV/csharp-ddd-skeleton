namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Threading.Tasks;
    using Domain.Bus.Event;

    public class RabbitMqDomainEventsConsumer : IDomainEventsConsumer
    {
        private readonly DomainEventInformation _domainEventInformation;
        private readonly InMemoryApplicationEventBus _bus;
        private readonly RabbitMqService _service;

        public RabbitMqDomainEventsConsumer(InMemoryApplicationEventBus bus,
            DomainEventInformation domainEventInformation, RabbitMqService service)
        {
            _bus = bus;
            _domainEventInformation = domainEventInformation;
            _service = service;
        }

        public async Task Consume()
        {
            _service.GetMessages("codelytv.mooc.coursecreated");
        }

        private async Task ExecuteSubscribers(DomainEventPrimitive domainEventPrimitive)
        {
        }
    }
}
namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqEventBusConfiguration
    {
        private DomainEventSubscribersInformation DomainEventSubscribersInformation;
        private string _exchangeName;
        private readonly RabbitMqService _service;

        public RabbitMqEventBusConfiguration(DomainEventSubscribersInformation domainEventSubscribersInformation,
            RabbitMqService service)
        {
            DomainEventSubscribersInformation = domainEventSubscribersInformation;
            _service = service;
            this._exchangeName = "domain_events";
        }

        public void SetUp()
        {
            var subscribersInformation = DomainEventSubscribersInformation.All();

            foreach (var domainEventSubscriberInformation in subscribersInformation)
            {
                var exchangeName = RabbitMqQueueNameFormatter.Format(domainEventSubscriberInformation);
                var retryExchangeName = RabbitMqQueueNameFormatter.FormatRetry(domainEventSubscriberInformation);
                var deadLetterExchangeName =
                    RabbitMqQueueNameFormatter.FormatDeadLetter(domainEventSubscriberInformation);

                _service.CreateQueueExchange(exchangeName, retryExchangeName, deadLetterExchangeName);
            }
        }
    }
}
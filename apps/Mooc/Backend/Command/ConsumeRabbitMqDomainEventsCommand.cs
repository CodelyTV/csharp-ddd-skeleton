namespace CodelyTv.Apps.Mooc.Backend.Command
{
    using Shared.Cli;
    using Shared.Infrastructure.Bus.Event.RabbitMq;

    public class ConsumeRabbitMqDomainEventsCommand : Command
    {
        private readonly RabbitMqDomainEventsConsumer _consumer;
        private readonly RabbitMqEventBusConfiguration _configuration;
        public ConsumeRabbitMqDomainEventsCommand(RabbitMqDomainEventsConsumer consumer, RabbitMqEventBusConfiguration configuration)
        {
            _consumer = consumer;
            _configuration = configuration;
            _configuration.SetUp();
        }

        public override void Execute(string[] args)
        {
            _consumer.Consume();
        }
    }
}
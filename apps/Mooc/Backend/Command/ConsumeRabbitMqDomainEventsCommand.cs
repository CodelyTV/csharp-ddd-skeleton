namespace CodelyTv.Apps.Mooc.Backend.Command
{
    using Shared.Cli;
    using Shared.Infrastructure.Bus.Event.RabbitMq;

    public class ConsumeRabbitMqDomainEventsCommand : Command
    {
        private readonly RabbitMqDomainEventsConsumer _consumer;

        public ConsumeRabbitMqDomainEventsCommand(RabbitMqDomainEventsConsumer consumer)
        {
            _consumer = consumer;
        }

        public override void Execute(string[] args)
        {
            _consumer.Consume();
        }
    }
}
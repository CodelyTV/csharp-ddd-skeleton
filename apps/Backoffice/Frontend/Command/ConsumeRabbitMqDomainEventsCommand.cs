namespace CodelyTv.Apps.Backoffice.Frontend.Command
{
    using CodelyTv.Shared.Cli;
    using CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq;

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
using CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq;

namespace CodelyTv.Apps.Backoffice.Frontend.Command
{
    public class ConsumeRabbitMqDomainEventsCommand : Shared.Cli.Command
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

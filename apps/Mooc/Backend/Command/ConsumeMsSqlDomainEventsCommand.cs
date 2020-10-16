using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;

namespace CodelyTv.Apps.Mooc.Backend.Command
{
    public class ConsumeMsSqlDomainEventsCommand : Shared.Cli.Command
    {
        private readonly MsSqlDomainEventsConsumer _consumer;

        public ConsumeMsSqlDomainEventsCommand(MsSqlDomainEventsConsumer consumer)
        {
            _consumer = consumer;
        }

        public override void Execute(string[] args)
        {
            _consumer.Consume();
        }
    }
}

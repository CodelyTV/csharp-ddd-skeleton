namespace CodelyTv.Apps.Mooc.Backend.Command
{
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Cli;
    using Shared.Infrastructure.Bus.Event.MsSql;

    public class ConsumeMsSqlDomainEventsCommand : Command
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
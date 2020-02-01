namespace CodelyTv.Apps.Mooc.Backend.Command
{
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Infrastructure.Bus.Event.MsSql;

    public class ConsumeMsSqlDomainEventsCommand : Command
    {
        private readonly MsSqlDomainEventsConsumer _consumer;

        public ConsumeMsSqlDomainEventsCommand(ServiceProvider provider)
        {
            _consumer = provider.GetService<MsSqlDomainEventsConsumer>();
        }

        public override void Execute(string[] args)
        {
            _consumer.Consume();
        }
    }
}
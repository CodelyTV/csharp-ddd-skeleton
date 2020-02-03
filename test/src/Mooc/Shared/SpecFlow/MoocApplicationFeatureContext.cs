namespace CodelyTv.Tests.Mooc.Shared.SpecFlow
{
    using Apps.Mooc.Backend;
    using Factory;
    using Test.Shared.Infrastructure.SpecFlow;

    public class MoocApplicationFeatureContext : ApplicationFeatureContext<Startup>
    {
        public MoocApplicationFeatureContext(MoocFactorySessionHelper moocFactoryEventBus)
        {
            this.EventBus = moocFactoryEventBus.EventBusContext;
            this.DomainEventDeserializer = moocFactoryEventBus.DomainEventDeserializer;
        }
    }
}
namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodelyTv.Shared.Domain.Bus.Event;
    using TechTalk.SpecFlow;

    [Binding]
    public abstract class EventBusContext<TStartup> where TStartup : class
    {
        private IEventBus _eventBus;

        protected EventBusContext(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [Given(@"I send a '(.*)' to the bus")]
        public async Task GivenISendAGetRequestTo(List<DomainEvent> events)
        {
            await this._eventBus.Publish(events);
        }
    }
}
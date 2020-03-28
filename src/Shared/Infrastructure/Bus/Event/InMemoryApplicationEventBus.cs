namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using Microsoft.Extensions.DependencyInjection;

    public class InMemoryApplicationEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryApplicationEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish(List<DomainEvent> events)
        {
            if (events == null)
                return;

            using IServiceScope scope = _serviceProvider.CreateScope();
            foreach (var @event in events)
            {
                var subscribers = GetSubscribers(@event, scope);

                foreach (object subscriber in subscribers)
                {
                    await ((IDomainEventSubscriberBase) subscriber).On(@event);
                }
            }
        }

        private static IEnumerable<object> GetSubscribers(DomainEvent @event, IServiceScope scope)
        {
            Type eventType = @event.GetType();
            Type subscriberType = typeof(IDomainEventSubscriber<>).MakeGenericType(eventType);
            return scope.ServiceProvider.GetServices(subscriberType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    public class InMemoryApplicationEventBus : EventBus
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

            using var scope = _serviceProvider.CreateScope();
            foreach (var @event in events)
            {
                var subscribers = GetSubscribers(@event, scope);

                foreach (var subscriber in subscribers) await ((DomainEventSubscriberBase) subscriber).On(@event);
            }
        }

        private static IEnumerable<object> GetSubscribers(DomainEvent @event, IServiceScope scope)
        {
            var eventType = @event.GetType();
            var subscriberType = typeof(DomainEventSubscriber<>).MakeGenericType(eventType);
            return scope.ServiceProvider.GetServices(subscriberType);
        }
    }
}

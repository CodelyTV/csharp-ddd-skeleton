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
                Type eventType = @event.GetType();
                Type suscriberType = typeof(IDomainEventSubscriber<>).MakeGenericType(eventType);
                IEnumerable<object> suscribers = scope.ServiceProvider.GetServices(suscriberType);

                foreach (object suscriber in suscribers)
                {
                    await ((IDomainEventSubscriberBase) suscriber).On(@event);
                }
            }
        }
    }
}
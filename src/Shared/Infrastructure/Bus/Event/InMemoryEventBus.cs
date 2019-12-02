namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using Microsoft.Extensions.DependencyInjection;

    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish(List<IDomainEvent> events)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                foreach (var @event in events)
                {
                    Type eventType = @event.GetType();
                    Type suscriberType = typeof(IDomainEventSuscriber<>).MakeGenericType(eventType);
                    IEnumerable<object> suscribers = scope.ServiceProvider.GetServices(suscriberType);

                    foreach (object suscriber in suscribers)
                    {
                        await Notify(suscriberType, suscriber, @event);
                    }
                }
            }
        }

        private async Task Notify(Type suscriberType, object suscriber, IDomainEvent @event)
        {
            object result = suscriberType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(IDomainEventSuscriber<IDomainEvent>.On))
                .Invoke(suscriber, new object[]
                {
                    @event
                });
            await (Task) result;
        }
    }
}
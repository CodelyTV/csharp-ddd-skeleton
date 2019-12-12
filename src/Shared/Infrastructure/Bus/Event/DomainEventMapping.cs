namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Bus.Event;
    using Microsoft.Extensions.DependencyInjection;

    public class DomainEventMapping
    {
        private IServiceProvider _serviceProvider;

        public DomainEventMapping()
        {
            var domainEvents = GetDomainEvents();
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                foreach (var @event in domainEvents)
                {
                    Type eventType = @event;
                    Type suscriberType = typeof(IDomainEventSuscriber<>).MakeGenericType(eventType);
                    IEnumerable<object> suscribers = scope.ServiceProvider.GetServices(suscriberType);

                    foreach (object suscriber in suscribers)
                    {
                        Console.WriteLine(suscriber);
                    }
                }
            }
        }

        private IEnumerable<Type> GetDomainEvents()
        {
            var type = typeof(DomainEvent);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);
            return types;
        }

        private IEnumerable<Type> GetSuscribers<T>() where T : DomainEvent
        {
            var type = typeof(IDomainEventSuscriber<T>);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
            return types;
        }
    }
}
namespace CodelyTv.Apps.Mooc.Backend.Extension.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Domain.Bus.Event;
    using Shared.Infrastructure.Bus.Event;

    public static class InMemoryEventBusExtension
    {
        public static IServiceCollection AddDomainEventSubscribersServices(this IServiceCollection services,
            Assembly assembly)
        {
            var information =
                new Dictionary<Type, DomainEventSubscriberInformation>();

            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerInterfaceType in interfaces.Where(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventSubscriber<>)))
                {
                    services.AddScoped(handlerInterfaceType.AsType(), type.AsType());
                }
            }

            services.AddScoped(s => new DomainEventSubscribersInformation(information));
            return services;
        }
    }
}
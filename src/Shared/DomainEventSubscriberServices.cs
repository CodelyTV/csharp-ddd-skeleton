namespace CodelyTv.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Domain.Bus.Event;
    using Infrastructure.Bus.Event;
    using Microsoft.Extensions.DependencyInjection;

    public static class DomainEventSubscriberInformationService
    {
        public static IServiceCollection AddDomainEventSubscriberInformationService(this IServiceCollection services,
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
                    FormatSubscribers(handlerInterfaceType, information);
                }
            }

            services.AddScoped(s => new DomainEventSubscribersInformation(information));
            return services;
        }

        private static void FormatSubscribers(TypeInfo handlerInterfaceType,
            Dictionary<Type, DomainEventSubscriberInformation> information)
        {
            var handlerClassTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s=> s.GetLoadableTypes())
                .Where(p => handlerInterfaceType.IsAssignableFrom(p));

            var eventType = handlerInterfaceType.GenericTypeArguments.FirstOrDefault();

            if (eventType == null) return;

            foreach (var handlerClassType in handlerClassTypes)
            {
                information.Add(handlerClassType,
                    new DomainEventSubscriberInformation(handlerClassType, eventType));
            }
        }
        
        private static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
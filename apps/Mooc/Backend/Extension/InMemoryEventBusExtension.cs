namespace CodelyTv.Apps.Mooc.Backend.Extension
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Domain.Bus.Event;

    public static class InMemoryEventBusExtension
    {
        public static IServiceCollection AddDomainEventSuscribersServices(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventSuscriber<>)))
                {
                    services.AddScoped(handlerType.AsType(), type.AsType());
                }
            }

            return services;
        }
    }
}
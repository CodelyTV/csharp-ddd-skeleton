using System.Linq;
using System.Reflection;
using CodelyTv.Shared.Domain.Bus.Query;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Shared
{
    public static class QueryServiceExtension
    {
        public static IServiceCollection AddQueryServices(this IServiceCollection services,
            Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerInterfaceType in interfaces.Where(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(QueryHandler<,>)))
                    services.AddScoped(handlerInterfaceType.AsType(), type.AsType());
            }

            return services;
        }
    }
}

using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Backoffice.Courses.Infrastructure.Persistence.Elasticsearch;
using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.Elasticsearch;
using CodelyTv.Shared.Domain.Bus.Query;
using CodelyTv.Shared.Infrastructure.Bus.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Apps.Backoffice.Backend.Extension.DependencyInjection
{
    public static class Infrastructure
    {
        internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<QueryBus, InMemoryQueryBus>();
            services.AddScoped<BackofficeCourseRepository, ElasticsearchBackofficeCourseRepository>();
            services.AddElasticsearch(configuration);
            return services;
        }
    }
}

using System;
using System.Linq;
using CodelyTv.Apps.Backoffice.Frontend;
using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Backoffice.Courses.Infrastructure.Persistence.Elasticsearch;
using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.Elasticsearch;
using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework;
using CodelyTv.Test.Backoffice.Shared.Infrastructure.Persistence;
using CodelyTv.Test.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace CodelyTv.Test.Backoffice
{
    public class BackofficeContextInfrastructureTestCase : InfrastructureTestCase<Startup>, IDisposable
    {
        protected override Action<IServiceCollection> Services()
        {
            return services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BackofficeContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddScoped<BackofficeCourseRepository, ElasticsearchBackofficeCourseRepository>();
                services.AddElasticsearch(configuration);
            };
        }

        protected override void Setup()
        {
            var cleaner = new ElasticDatabaseCleaner(GetService<ElasticClient>());
            cleaner.Execute();
        }

        public void Dispose()
        {
            Finish();
        }
    }
}

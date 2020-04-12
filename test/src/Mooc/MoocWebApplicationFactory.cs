namespace CodelyTv.Test.Mooc
{
    using System;
    using System.Net.Http;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared.Infrastructure;

    public class MoocWebApplicationFactory<TStartup> : ApplicationTestCase<TStartup> where TStartup : class
    {
        private string _databaseName;

        public HttpClient GetAnonymousClient()
        {
            SetDatabaseName();
            return CreateClient();
        }

        private void SetDatabaseName()
        {
            this._databaseName = Guid.NewGuid().ToString();
        }

        protected override Action<IServiceCollection> Services()
        {
            return services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context using an in-memory 
                // database for testing.
                services.AddDbContext<MoocContext>(options =>
                {
                    options.UseInMemoryDatabase(_databaseName);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<IEventBus, InMemoryApplicationEventBus>();

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<MoocContext>();

                // Ensure the database is created.
                context.Database.EnsureCreated();
            };
        }
    }
}
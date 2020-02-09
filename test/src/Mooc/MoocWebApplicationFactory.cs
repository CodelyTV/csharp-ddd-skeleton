namespace CodelyTv.Tests.Mooc
{
    using System;
    using System.Net.Http;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared;

    public class MoocWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private string _databaseName;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
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
                });
        }

        public HttpClient GetAnonymousClient()
        {
            SetRandomDatabaseName();
            return CreateClient();
        }

        private void SetRandomDatabaseName()
        {
            _databaseName = Guid.NewGuid().ToString();
        }
    }
}
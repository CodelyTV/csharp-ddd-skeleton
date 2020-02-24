namespace CodelyTv.Tests.Mooc
{
    using System;
    using System.Linq;
    using Apps.Mooc.Backend;
    using Apps.Mooc.Backend.Extension;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared.Infrastructure;

    public class MoocContextInfrastructureTestCase : InfrastructureTestCase<Startup>
    {
        protected override Action<IServiceCollection> Services()
        {
            return services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MoocContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddScoped<MsSqlEventBus, MsSqlEventBus>();
                services.AddScoped<IDomainEventsConsumer, MsSqlDomainEventsConsumer>();

                services.AddScoped<DomainEventInformation, DomainEventInformation>();
                services.AddScoped<IEventBus, InMemoryApplicationEventBus>();

                services.AddDomainEventSubscribersServices(AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.FullName.Contains("CodelyTv.Mooc")));
                
                services.AddDbContext<MoocContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MoocDatabase")));
            };
        }
    }
}
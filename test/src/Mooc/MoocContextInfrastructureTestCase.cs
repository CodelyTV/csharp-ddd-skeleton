namespace CodelyTv.Tests.Mooc
{
    using System;
    using System.Linq;
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
    using Microsoft.EntityFrameworkCore;
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

                services.AddScoped<IEventBus, MsSqlEventBus>();
                services.AddScoped<IDomainEventsConsumer, MsSqlDomainEventsConsumer>();
                services.AddScoped<DomainEventInformation, DomainEventInformation>();
                services.AddScoped<IEventBus, InMemoryApplicationEventBus>();

                services.AddDbContext<MoocContext>(options => options.UseInMemoryDatabase("TestingDB"));
            };
        }
    }
}
using System;
using System.Linq;
using CodelyTv.Apps.Mooc.Backend;
using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
using CodelyTv.Shared;
using CodelyTv.Shared.Helpers;
using CodelyTv.Shared.Infrastructure.Bus.Event;
using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
using CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq;
using CodelyTv.Test.Mooc.Shared.Infrastructure.Bus.Event.RabbitMq;
using CodelyTv.Test.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Test.Mooc
{
    public class MoocContextInfrastructureTestCase : InfrastructureTestCase<Startup>, IDisposable
    {
        protected override void Setup()
        {
        }

        protected override Action<IServiceCollection> Services()
        {
            return services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MoocContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddScoped<MsSqlEventBus, MsSqlEventBus>();
                services.AddScoped<MsSqlDomainEventsConsumer, MsSqlDomainEventsConsumer>();

                services.AddScoped(p =>
                {
                    var publisher = p.GetRequiredService<RabbitMqPublisher>();
                    var failOverBus = p.GetRequiredService<MsSqlEventBus>();
                    return new RabbitMqEventBus(publisher, failOverBus, "test_domain_events");
                });

                services.AddScoped<EventBusConfiguration, RabbitMqEventBusConfiguration>();

                services.AddScoped<DomainEventsInformation, DomainEventsInformation>();

                services.AddScoped<TestAllWorksOnRabbitMqEventsPublished, TestAllWorksOnRabbitMqEventsPublished>();

                services.AddDomainEventSubscriberInformationService(AssemblyHelper.GetInstance(Assemblies.Mooc));
                services.AddCommandServices(AssemblyHelper.GetInstance(Assemblies.Mooc));
                services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Mooc));

                services.AddDbContext<MoocContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MoocDatabase"), options => options.EnableRetryOnFailure()));

                services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));
            };
        }

        public void Dispose()
        {
            Finish();
        }
    }
}

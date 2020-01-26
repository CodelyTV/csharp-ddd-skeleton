namespace CodelyTv.Tests.Mooc
{
    using System.Linq;
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Test.Shared.Infrastructure.XUnit;

    public abstract class MoocContextInfrastructureTestCase : InfrastructureTestCase<Startup>
    {
        protected IHost Host { get; private set; }

        public MoocContextInfrastructureTestCase()
        {
            this.SetUp();
        }

        private void SetUp()
        {
            this.Host = CreateHost(CreateHostServices<MoocContext>).GetAwaiter().GetResult();
        }

        protected override void CreateHostServices<TDbContext>(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddScoped<IEventBus, MsSqlEventBus>();
            services.AddScoped<IDomainEventsConsumer, MsSqlDomainEventsConsumer>();
            services.AddScoped<DomainEventInformation, DomainEventInformation>();
            services.AddScoped<IEventBus, InMemoryApplicationEventBus>();

            services.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
        }
    }
}
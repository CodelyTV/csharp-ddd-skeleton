namespace CodelyTv.Tests.Mooc.Shared.Factory
{
    using System.Linq;
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Test.Shared.Infrastructure;
    using Test.Shared.Infrastructure.Factory;

    public class MoocFactorySessionHelper : SessionHelper<Startup>
    {
        private IHost Host { get; set; }

        public IEventBus EventBusContext { get; private set; }
        public IDomainEventDeserializer DomainEventDeserializer { get; private set; }

        public MoocFactorySessionHelper()
        {
            this.SetUp();
        }

        private void SetUp()
        {
            this.Host = CreateHost(CreateHostServices<MoocContext>).GetAwaiter().GetResult();

            this.Client = this.Host.GetTestClient();

            this.EventBusContext = this.Host.Services.GetService(typeof(IEventBus)) as IEventBus;
            this.DomainEventDeserializer = this.Host.Services.GetService(typeof(IDomainEventDeserializer)) as IDomainEventDeserializer;
        }

        protected override void CreateHostServices<TDbContext>(IServiceCollection services)
        {
            services.AddScoped<IRandomNumberGenerator, ConstantNumberGenerator>();
            services.AddScoped<IEventBus, InMemoryEventBus>();
            services.AddScoped<DomainEventInformation, DomainEventInformation>();
            services.AddScoped<IDomainEventDeserializer, DomainEventJsonDeserializer>();

            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
        }
    }
}
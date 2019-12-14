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
    using Test.Shared.Infrastructure;
    using Test.Shared.Infrastructure.Factory;

    public class MoocFactorySessionHelper : SessionHelper<Startup>
    {
        private TestServer TestServer { get; }

        public IEventBus EventBusContext { get; private set; }
        public IDomainEventDeserializer DomainEventDeserializer { get; private set; }

        public MoocFactorySessionHelper()
        {
            this.TestServer = base.CreateServer(CreateTestServices<MoocContext>);
            this.SetUp();
        }

        private void SetUp()
        {
            this.Client = this.TestServer.CreateClient();
            this.EventBusContext = this.TestServer.Host.Services.GetService(typeof(IEventBus)) as IEventBus;
            this.DomainEventDeserializer = this.TestServer.Host.Services.GetService(typeof(IDomainEventDeserializer)) as IDomainEventDeserializer;
        }

        protected override void CreateTestServices<TDbContext>(IServiceCollection services)
        {
            services.AddScoped<IRandomNumberGenerator, ConstantNumberGenerator>();
            services.AddScoped<IEventBus, InMemoryEventBus>();
            services.AddScoped<IDomainEventDeserializer, DomainEventJsonDeserializer>();
            services.AddScoped<DomainEventInformation, DomainEventInformation>();

            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
        }
    }
}
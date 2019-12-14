namespace CodelyTv.Tests.Mooc.Shared.XUnit
{
    using System.Linq;
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared.Infrastructure.XUnit;

    public abstract class MoocContextInfrastructureTestCase : InfrastructureTestCase<Startup>
    {
        protected TestServer TestServer { get; }

        public MoocContextInfrastructureTestCase()
        {
            this.TestServer = base.CreateServer(CreateTestServices<MoocContext>);
        }

        protected override void CreateTestServices<TDbContext>(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase("TestingDB"));
        }
    }
}
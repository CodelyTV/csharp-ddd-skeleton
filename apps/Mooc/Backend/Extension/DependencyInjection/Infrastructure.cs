namespace CodelyTv.Apps.Mooc.Backend.Extension.DependencyInjection
{
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Mooc.Courses.Infrastructure.Persistence;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using CodelyTv.Mooc.CoursesCounter.Infrastructure.Persistence;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Domain;
    using Shared.Domain.Bus.Event;
    using Shared.Infrastructure;
    using Shared.Infrastructure.Bus.Event;
    using Shared.Infrastructure.Bus.Event.MsSql;
    using Shared.Infrastructure.Bus.Event.RabbitMq;

    public static class Infrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<IUuidGenerator, CSharpUuidGenerator>();
            services.AddScoped<ICoursesCounterRepository, MsSqlCoursesCounterRepository>();
            services.AddScoped<ICourseRepository, MsSqlCourseRepository>();
            services.AddScoped<IEventBus, RabbitMqEventBus>();
            services.AddScoped<InMemoryApplicationEventBus, InMemoryApplicationEventBus>();

            services.AddScoped<IDomainEventsConsumer, MsSqlDomainEventsConsumer>();
            services.AddScoped<DomainEventInformation, DomainEventInformation>();

            services.AddScoped<DbContext, MoocContext>();
            services.AddDbContext<MoocContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MoocDatabase")));

            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));
            
            return services;
        }
    }
}
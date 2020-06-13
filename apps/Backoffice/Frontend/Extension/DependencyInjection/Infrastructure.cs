namespace CodelyTv.Apps.Backoffice.Frontend.Extension.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mooc.Courses.Domain;
    using Mooc.Courses.Infrastructure.Persistence;
    using Mooc.CoursesCounter.Domain;
    using Mooc.CoursesCounter.Infrastructure.Persistence;
    using Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Shared.Domain;
    using Shared.Domain.Bus.Command;
    using Shared.Domain.Bus.Event;
    using Shared.Domain.Bus.Query;
    using Shared.Infrastructure;
    using Shared.Infrastructure.Bus.Command;
    using Shared.Infrastructure.Bus.Event;
    using Shared.Infrastructure.Bus.Event.MsSql;
    using Shared.Infrastructure.Bus.Event.RabbitMq;
    using Shared.Infrastructure.Bus.Query;

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
            services.AddScoped<IEventBusConfiguration, RabbitMqEventBusConfiguration>();
            services.AddScoped<InMemoryApplicationEventBus, InMemoryApplicationEventBus>();
            
            // Failover
            services.AddScoped<MsSqlEventBus, MsSqlEventBus>();
            
            services.AddScoped<RabbitMqDomainEventsConsumer, RabbitMqDomainEventsConsumer>();
            services.AddScoped<DomainEventsInformation, DomainEventsInformation>();

            services.AddScoped<DbContext, MoocContext>();
            services.AddDbContext<MoocContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MoocDatabase")), ServiceLifetime.Transient);

            services.AddRabbitMq(configuration);

            services.AddScoped<DomainEventJsonDeserializer, DomainEventJsonDeserializer>();
            services.AddScoped<ICommandBus, InMemoryCommandBus>();
            services.AddScoped<IQueryBus, InMemoryQueryBus>();
            
            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<RabbitMqPublisher, RabbitMqPublisher>();
            services.AddScoped<RabbitMqConfig, RabbitMqConfig>();
            services.Configure<RabbitMqConfigParams>(configuration.GetSection("RabbitMq"));

            return services;
        }
    }
}
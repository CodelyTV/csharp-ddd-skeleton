namespace CodelyTv.Apps.Backoffice.Frontend.Extension.DependencyInjection
{
    using CodelyTv.Backoffice.Courses.Domain;
    using CodelyTv.Backoffice.Courses.Infrastructure.Persistence.Elasticsearch;
    using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.Elasticsearch;
    using CodelyTv.Backoffice.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Mooc.Courses.Infrastructure.Persistence;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using CodelyTv.Mooc.CoursesCounter.Infrastructure.Persistence;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using CodelyTv.Shared.Domain;
    using CodelyTv.Shared.Domain.Bus.Command;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Domain.Bus.Query;
    using CodelyTv.Shared.Infrastructure;
    using CodelyTv.Shared.Infrastructure.Bus.Command;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
    using CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq;
    using CodelyTv.Shared.Infrastructure.Bus.Query;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Infrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<IUuidGenerator, CSharpUuidGenerator>();
            services.AddScoped<IBackofficeCourseRepository, ElasticsearchBackofficeCourseRepository>();
            services.AddScoped<ICoursesCounterRepository, MsSqlCoursesCounterRepository>();
            services.AddScoped<ICourseRepository, MsSqlCourseRepository>();

            services.AddScoped<CoursesCounterFinder, CoursesCounterFinder>();
            
            services.AddScoped<IEventBus, RabbitMqEventBus>();
            services.AddScoped<IEventBusConfiguration, RabbitMqEventBusConfiguration>();
            services.AddScoped<InMemoryApplicationEventBus, InMemoryApplicationEventBus>();
            
            // Failover
            services.AddScoped<MsSqlEventBus, MsSqlEventBus>();
            
            services.AddScoped<RabbitMqDomainEventsConsumer, RabbitMqDomainEventsConsumer>();
            services.AddScoped<DomainEventsInformation, DomainEventsInformation>();

            services.AddScoped<DbContext, BackofficeContext>();
            services.AddDbContext<BackofficeContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BackofficeDatabase")), ServiceLifetime.Transient);
            
            services.AddScoped<MoocContext, MoocContext>();
            services.AddDbContext<MoocContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MoocDatabase")), ServiceLifetime.Transient);

            services.AddRabbitMq(configuration);
            services.AddScoped<DomainEventJsonDeserializer, DomainEventJsonDeserializer>();
            services.AddScoped<ICommandBus, InMemoryCommandBus>();
            services.AddScoped<IQueryBus, InMemoryQueryBus>();
            
            services.AddElasticsearch(configuration);

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
using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Mooc.Courses.Infrastructure.Persistence;
using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Mooc.CoursesCounters.Infrastructure.Persistence;
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

namespace CodelyTv.Apps.Mooc.Backend.Extension.DependencyInjection
{
    public static class Infrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<RandomNumberGenerator, CSharpRandomNumberGenerator>();
            services.AddScoped<UuidGenerator, CSharpUuidGenerator>();
            services.AddScoped<CoursesCounterRepository, MsSqlCoursesCounterRepository>();
            services.AddScoped<CourseRepository, MsSqlCourseRepository>();

            services.AddScoped<EventBus, RabbitMqEventBus>();
            services.AddScoped<EventBusConfiguration, RabbitMqEventBusConfiguration>();
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
            services.AddScoped<CommandBus, InMemoryCommandBus>();
            services.AddScoped<QueryBus, InMemoryQueryBus>();

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

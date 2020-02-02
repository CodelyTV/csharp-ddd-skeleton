namespace CodelyTv.Apps.Mooc.Backend.Extension.DependencyInjection
{
    using System;
    using System.Linq;
    using CodelyTv.Mooc.Courses.Application.Create;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Mooc.CoursesCounter.Application.Incrementer;
    using Microsoft.Extensions.DependencyInjection;

    public static class Application
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CourseCreator, CourseCreator>();

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<CoursesCounterFinder, CoursesCounterFinder>();
            
            services.AddDomainEventSubscribersServices(AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("CodelyTv.Mooc")));

            return services;
        }
    }
}
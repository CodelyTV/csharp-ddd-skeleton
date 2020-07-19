namespace CodelyTv.Apps.Mooc.Backend.Extension.DependencyInjection
{
    using CodelyTv.Mooc.Courses.Application.Create;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Mooc.CoursesCounter.Application.Incrementer;
    using CodelyTv.Shared;
    using CodelyTv.Shared.Helpers;
    using Microsoft.Extensions.DependencyInjection;

    public static class Application
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CourseCreator, CourseCreator>();

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<CoursesCounterFinder, CoursesCounterFinder>();
            services.AddScoped<IncrementCoursesCounterOnCourseCreated, IncrementCoursesCounterOnCourseCreated>();
            
            services.AddDomainEventSubscriberInformationService(AssemblyHelper.GetInstance(Assemblies.Mooc));
            services.AddCommandServices(AssemblyHelper.GetInstance(Assemblies.Mooc));
            services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Mooc));
            return services;
        }
    }
}
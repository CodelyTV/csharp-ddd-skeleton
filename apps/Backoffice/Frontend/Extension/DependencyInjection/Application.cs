namespace CodelyTv.Apps.Backoffice.Frontend.Extension.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Mooc.Courses.Application.Create;
    using Mooc.CoursesCounter.Application.Find;
    using Mooc.CoursesCounter.Application.Incrementer;
    using Mooc.Helper;
    using Shared;

    public static class Application
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CourseCreator, CourseCreator>();

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<CoursesCounterFinder, CoursesCounterFinder>();
            services.AddScoped<IncrementCoursesCounterOnCourseCreated, IncrementCoursesCounterOnCourseCreated>();
            
            services.AddDomainEventSubscriberInformationService(MoocAssemblyHelper.Instance());
            services.AddCommandServices(MoocAssemblyHelper.Instance());
            services.AddQueryServices(MoocAssemblyHelper.Instance());
            return services;
        }
    }
}
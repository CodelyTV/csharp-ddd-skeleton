namespace CodelyTv.Apps.Backoffice.Frontend.Extension.DependencyInjection
{
    using CodelyTv.Backoffice.Courses.Application.Create;
    using CodelyTv.Backoffice.Courses.Application.SearchAll;
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
            services.AddScoped<CreateBackofficeCourseOnCourseCreated, CreateBackofficeCourseOnCourseCreated>();
            services.AddScoped<CourseCreator, CourseCreator>();

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<CoursesCounterFinder, CoursesCounterFinder>();
            services.AddScoped<IncrementCoursesCounterOnCourseCreated, IncrementCoursesCounterOnCourseCreated>();
            
            return services;
        }
    }
}
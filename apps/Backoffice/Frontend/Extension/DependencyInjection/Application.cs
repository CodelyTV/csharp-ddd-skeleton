using CodelyTv.Backoffice.Courses.Application.Create;
using CodelyTv.Backoffice.Courses.Application.SearchAll;
using CodelyTv.Backoffice.Courses.Application.SearchByCriteria;
using CodelyTv.Mooc.Courses.Application.Create;
using CodelyTv.Mooc.CoursesCounters.Application.Find;
using CodelyTv.Mooc.CoursesCounters.Application.Incrementer;
using CodelyTv.Shared;
using CodelyTv.Shared.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Apps.Backoffice.Frontend.Extension.DependencyInjection
{
    public static class Application
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateBackofficeCourseOnCourseCreated, CreateBackofficeCourseOnCourseCreated>();
            services.AddScoped<CourseCreator, CourseCreator>();
            services.AddScoped<BackofficeCourseCreator, BackofficeCourseCreator>();
            services.AddScoped<AllBackofficeCoursesSearcher, AllBackofficeCoursesSearcher>();
            services.AddScoped<BackofficeCoursesByCriteriaSearcher, BackofficeCoursesByCriteriaSearcher>();

            services.AddDomainEventSubscriberInformationService(AssemblyHelper.GetInstance(Assemblies.Backoffice));
            services.AddCommandServices(AssemblyHelper.GetInstance(Assemblies.Backoffice));
            services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Backoffice));

            services.AddCommandServices(AssemblyHelper.GetInstance(Assemblies.Mooc));
            services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Mooc));

            services.AddScoped<CoursesCounterIncrementer, CoursesCounterIncrementer>();
            services.AddScoped<CoursesCounterFinder, CoursesCounterFinder>();
            services.AddScoped<IncrementCoursesCounterOnCourseCreated, IncrementCoursesCounterOnCourseCreated>();

            return services;
        }
    }
}

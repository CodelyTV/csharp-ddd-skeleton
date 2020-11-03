using CodelyTv.Backoffice.Courses.Application.SearchAll;
using CodelyTv.Backoffice.Courses.Application.SearchByCriteria;
using CodelyTv.Shared;
using CodelyTv.Shared.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Apps.Backoffice.Backend.Extension.DependencyInjection
{
    public static class Application
    {
        internal static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Backoffice));
            services.AddScoped<BackofficeCoursesByCriteriaSearcher, BackofficeCoursesByCriteriaSearcher>();
            services.AddScoped<AllBackofficeCoursesSearcher, AllBackofficeCoursesSearcher>();

            return services;
        }
    }
}

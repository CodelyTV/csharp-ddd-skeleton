using CodelyTv.Backoffice.Courses.Domain;

namespace CodelyTv.Test.Backoffice.Courses
{
    public class BackofficeCoursesModuleInfrastructureTestCase : BackofficeContextInfrastructureTestCase
    {
        protected BackofficeCourseRepository ElasticRepository => GetService<BackofficeCourseRepository>();
    }
}

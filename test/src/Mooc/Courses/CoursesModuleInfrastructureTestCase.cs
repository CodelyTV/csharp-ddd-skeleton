using CodelyTv.Mooc.Courses.Domain;

namespace CodelyTv.Test.Mooc.Courses
{
    public abstract class CoursesModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected ICourseRepository Repository => GetService<ICourseRepository>();
    }
}

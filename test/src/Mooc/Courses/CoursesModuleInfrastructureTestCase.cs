using CodelyTv.Mooc.Courses.Domain;

namespace CodelyTv.Test.Mooc.Courses
{
    public abstract class CoursesModuleInfrastructureTestCase : MoocContextInfrastructureTestCase
    {
        protected CourseRepository Repository => GetService<CourseRepository>();
    }
}

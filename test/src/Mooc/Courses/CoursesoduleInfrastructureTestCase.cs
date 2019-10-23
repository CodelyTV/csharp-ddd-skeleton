namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Infrastructure;

    public abstract class CoursesModuleInfrastructureTestCase
    {
        protected FileCourseRepository Repository = new FileCourseRepository();
    }
}
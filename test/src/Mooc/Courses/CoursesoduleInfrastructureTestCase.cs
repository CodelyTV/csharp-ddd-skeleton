namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Infrastructure;

    public abstract class CoursesModuleInfrastructureTestCase
    {
        protected readonly FileCourseRepository Repository = new FileCourseRepository();
    }
}
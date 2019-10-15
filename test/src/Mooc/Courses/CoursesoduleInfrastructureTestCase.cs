namespace MoocTest.src.Courses
{
    using Mooc.Courses.Infrastructure;

    public abstract class CoursesModuleInfrastructureTestCase
    {
        protected FileCourseRepository Repository = new FileCourseRepository();
    }
}
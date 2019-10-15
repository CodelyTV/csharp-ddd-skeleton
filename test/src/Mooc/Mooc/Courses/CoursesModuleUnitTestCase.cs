namespace MoocTest.src.Courses
{
    using FakeItEasy;
    using Mooc.Courses.Domain;

    public abstract class CoursesModuleUnitTestCase
    {
        protected ICourseRepository Repository = A.Fake<ICourseRepository>();

        protected void ShouldSave(Course course)
        {
            A.CallTo(() => this.Repository.Save(course));
        }
    }
}
namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Domain;
    using FakeItEasy;

    public abstract class CoursesModuleUnitTestCase
    {
        protected ICourseRepository Repository = A.Fake<ICourseRepository>();

        protected void ShouldSave(Course course)
        {
            A.CallTo(() => this.Repository.Save(course));
        }
    }
}
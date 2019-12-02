namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Domain;
    using FakeItEasy;
    using Test.Shared.Infrastructure;

    public abstract class CoursesModuleUnitTestCase : UnitTestCase
    {
        protected readonly ICourseRepository Repository = A.Fake<ICourseRepository>();

        protected void ShouldSave(Course course)
        {
            A.CallTo(() => this.Repository.Save(course));
        }
    }
}
namespace CodelyTv.Tests.Mooc.Courses
{
    using CodelyTv.Mooc.Courses.Domain;
    using Moq;
    using Test.Shared.Infrastructure;

    public abstract class CoursesModuleUnitTestCase : UnitTestCase
    {
        protected readonly Mock<ICourseRepository> Repository;

        protected CoursesModuleUnitTestCase()
        {
            this.Repository = new Mock<ICourseRepository>();
        }

        protected void ShouldHaveSave(Course course)
        {
            this.Repository.Verify(x => x.Save(course), Times.AtLeastOnce());
        }
    }
}
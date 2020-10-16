using CodelyTv.Mooc.Courses.Domain;
using CodelyTv.Test.Shared.Infrastructure;
using Moq;

namespace CodelyTv.Test.Mooc.Courses
{
    public abstract class CoursesModuleUnitTestCase : UnitTestCase
    {
        protected readonly Mock<ICourseRepository> Repository;

        protected CoursesModuleUnitTestCase()
        {
            Repository = new Mock<ICourseRepository>();
        }

        protected void ShouldHaveSave(Course course)
        {
            Repository.Verify(x => x.Save(course), Times.AtLeastOnce());
        }
    }
}

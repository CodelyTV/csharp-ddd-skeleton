using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Test.Shared.Infrastructure;
using Moq;

namespace CodelyTv.Test.Mooc.CoursesCounters
{
    public class CoursesCounterModuleUnitTestCase : UnitTestCase
    {
        protected readonly Mock<CoursesCounterRepository> Repository;

        protected CoursesCounterModuleUnitTestCase()
        {
            Repository = new Mock<CoursesCounterRepository>();
        }

        protected void ShouldHaveSaved(CoursesCounter course)
        {
            Repository.Verify(x => x.Save(course), Times.AtLeastOnce());
        }

        protected void ShouldSearch(CoursesCounter counter)
        {
            Repository.Setup(x => x.Search()).ReturnsAsync(counter);
        }

        protected void ShouldSearch()
        {
            Repository.Setup(x => x.Search()).ReturnsAsync((CoursesCounter) null);
        }
    }
}

namespace CodelyTv.Test.Mooc.CoursesCounter
{
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Moq;
    using Test.Shared.Infrastructure;

    public class CoursesCounterModuleUnitTestCase : UnitTestCase
    {
        protected readonly Mock<ICoursesCounterRepository> Repository;

        protected CoursesCounterModuleUnitTestCase()
        {
            this.Repository = new Mock<ICoursesCounterRepository>();
        }

        protected void ShouldHaveSaved(CoursesCounter course)
        {
            this.Repository.Verify(x => x.Save(course), Times.AtLeastOnce());
        }

        protected void ShouldSearch(CoursesCounter counter)
        {
            this.Repository.Setup(x => x.Search()).ReturnsAsync(counter);
        }

        protected void ShouldSearch()
        {
            this.Repository.Setup(x => x.Search()).ReturnsAsync((CoursesCounter) null);
        }
    }
}
namespace CodelyTv.Tests.Mooc.CoursesCounter
{
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using FakeItEasy;
    using Test.Shared.Infrastructure;

    public class CoursesCounterModuleUnitTestCase : UnitTestCase
    {
        protected readonly ICoursesCounterRepository Repository = A.Fake<ICoursesCounterRepository>();

        protected void ShouldSave(CoursesCounter counter)
        {
            A.CallTo(() => this.Repository.Save(counter));
        }

        protected void ShouldSearch(CoursesCounter counter)
        {
            A.CallTo(() => this.Repository.Search()).Returns(counter);
        }
    }
}
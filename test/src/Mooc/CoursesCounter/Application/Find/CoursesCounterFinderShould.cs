namespace CodelyTv.Tests.Mooc.CoursesCounter.Application.Find
{
    using System.Threading.Tasks;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Domain;
    using Xunit;

    public class CoursesCounterFinderShould : CoursesCounterModuleUnitTestCase
    {
        private readonly CoursesCounterFinder _finder;

        public CoursesCounterFinderShould()
        {
            _finder = new CoursesCounterFinder(this.Repository.Object);
        }

        [Fact]
        public async Task it_should_find_an_existing_courses_counter()
        {
            CoursesCounter counter = CoursesCounterMother.Random();
            CoursesCounterResponse response = CoursesCounterResponseMother.Create(counter.Total.Value);

            ShouldSearch(counter);

            Assert.Equal(response, await _finder.Find());
        }

        [Fact]
        public async Task it_should_throw_an_exception_when_courses_counter_does_not_exists()
        {
            ShouldSearch();

            await Assert.ThrowsAsync<CoursesCounterNotInitialized>(async() => await this._finder.Find());
        }
    }
}
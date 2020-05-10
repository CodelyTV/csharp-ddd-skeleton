namespace CodelyTv.Test.Mooc.CoursesCounter.Application.Find
{
    using System.Threading.Tasks;
    using CodelyTv.Mooc.CoursesCounter.Application.Find;
    using CodelyTv.Mooc.CoursesCounter.Domain;
    using Domain;
    using Xunit;

    public class FindCoursesCounterQueryHandlerShould : CoursesCounterModuleUnitTestCase
    {
        private readonly FindCoursesCounterQueryHandler _handler;

        public FindCoursesCounterQueryHandlerShould()
        {
            _handler = new FindCoursesCounterQueryHandler(new CoursesCounterFinder(this.Repository.Object));
        }

        [Fact]
        public async Task it_should_find_an_existing_courses_counter()
        {
            CoursesCounter counter = CoursesCounterMother.Random();
            FindCoursesCounterQuery query = new FindCoursesCounterQuery();

            CoursesCounterResponse response = CoursesCounterResponseMother.Create(counter.Total.Value);

            ShouldSearch(counter);

            Assert.Equal(response, await _handler.Handle(query));
        }

        [Fact]
        public async Task it_should_throw_an_exception_when_courses_counter_does_not_exists()
        {
            FindCoursesCounterQuery query = new FindCoursesCounterQuery();

            ShouldSearch();

            await Assert.ThrowsAsync<CoursesCounterNotInitialized>(async () => await _handler.Handle(query));
        }
    }
}
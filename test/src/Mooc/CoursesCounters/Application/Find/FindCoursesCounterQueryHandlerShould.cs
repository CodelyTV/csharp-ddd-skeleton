using System.Threading.Tasks;
using CodelyTv.Mooc.CoursesCounters.Application.Find;
using CodelyTv.Mooc.CoursesCounters.Domain;
using CodelyTv.Test.Mooc.CoursesCounters.Domain;
using Xunit;

namespace CodelyTv.Test.Mooc.CoursesCounters.Application.Find
{
    public class FindCoursesCounterQueryHandlerShould : CoursesCounterModuleUnitTestCase
    {
        private readonly FindCoursesCounterQueryHandler _handler;

        public FindCoursesCounterQueryHandlerShould()
        {
            _handler = new FindCoursesCounterQueryHandler(new CoursesCounterFinder(Repository.Object));
        }

        [Fact]
        public async Task it_should_find_an_existing_courses_counter()
        {
            var counter = CoursesCounterMother.Random();
            var query = new FindCoursesCounterQuery();

            var response = CoursesCounterResponseMother.Create(counter.Total.Value);

            ShouldSearch(counter);

            Assert.Equal(response, await _handler.Handle(query));
        }

        [Fact]
        public async Task it_should_throw_an_exception_when_courses_counter_does_not_exists()
        {
            var query = new FindCoursesCounterQuery();

            ShouldSearch();

            await Assert.ThrowsAsync<CoursesCounterNotInitialized>(async () => await _handler.Handle(query));
        }
    }
}

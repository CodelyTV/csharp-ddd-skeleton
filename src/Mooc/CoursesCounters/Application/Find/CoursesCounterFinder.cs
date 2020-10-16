using System.Threading.Tasks;
using CodelyTv.Mooc.CoursesCounters.Domain;

namespace CodelyTv.Mooc.CoursesCounters.Application.Find
{
    public class CoursesCounterFinder
    {
        private readonly ICoursesCounterRepository _repository;

        public CoursesCounterFinder(ICoursesCounterRepository repository)
        {
            _repository = repository;
        }

        public async Task<CoursesCounterResponse> Find()
        {
            var coursesCounter = await _repository.Search() ?? throw new CoursesCounterNotInitialized();

            return new CoursesCounterResponse(coursesCounter.Total.Value);
        }
    }
}

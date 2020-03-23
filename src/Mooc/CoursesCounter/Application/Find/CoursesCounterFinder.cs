namespace CodelyTv.Mooc.CoursesCounter.Application.Find
{
    using System.Threading.Tasks;
    using Domain;

    public class CoursesCounterFinder
    {
        private ICoursesCounterRepository _repository;

        public CoursesCounterFinder(ICoursesCounterRepository repository)
        {
            _repository = repository;
        }

        public async Task<CoursesCounterResponse> Find()
        {
            CoursesCounter coursesCounter = await this._repository.Search() ?? throw new CoursesCounterNotInitialized();

            return new CoursesCounterResponse(coursesCounter.Total.Value);
        }
    }
}
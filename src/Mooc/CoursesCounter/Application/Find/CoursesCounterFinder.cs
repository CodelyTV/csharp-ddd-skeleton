namespace CodelyTv.Mooc.CoursesCounter.Application.Find
{
    using Domain;

    public class CoursesCounterFinder
    {
        private ICoursesCounterRepository _repository;

        public CoursesCounterFinder(ICoursesCounterRepository repository)
        {
            _repository = repository;
        }

        public CoursesCounterResponse Find()
        {
            CoursesCounter coursesCounter = this._repository.Search() ?? throw new CoursesCounterNotInitialized();

            return new CoursesCounterResponse(coursesCounter.Total.Value);
        }
    }
}
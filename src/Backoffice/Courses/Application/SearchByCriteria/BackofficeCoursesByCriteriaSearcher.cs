namespace CodelyTv.Backoffice.Courses.Application.SearchByCriteria
{
    using System.Linq;
    using System.Threading.Tasks;
    using CodelyTv.Backoffice.Courses.Domain;
    using CodelyTv.Shared.Domain.FiltersByCriteria;

    public class BackofficeCoursesByCriteriaSearcher
    {
        private readonly IBackofficeCourseRepository _repository;

        public BackofficeCoursesByCriteriaSearcher(IBackofficeCourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<BackofficeCoursesResponse> Search(Filters filter, Order order, int? limit, int? offset)
        {
            Criteria criteria = new Criteria(filter, order, limit, offset);

            return new BackofficeCoursesResponse((await _repository.Matching(criteria)).Select(BackofficeCourseResponse.FromAggregate));
        }
    }
}
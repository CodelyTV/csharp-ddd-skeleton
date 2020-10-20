using System.Linq;
using System.Threading.Tasks;
using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Backoffice.Courses.Application.SearchByCriteria
{
    public class BackofficeCoursesByCriteriaSearcher
    {
        private readonly BackofficeCourseRepository _repository;

        public BackofficeCoursesByCriteriaSearcher(BackofficeCourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<BackofficeCoursesResponse> Search(Filters filter, Order order, int? limit, int? offset)
        {
            var criteria = new Criteria(filter, order, limit, offset);

            return new BackofficeCoursesResponse(
                (await _repository.Matching(criteria)).Select(BackofficeCourseResponse.FromAggregate));
        }
    }
}

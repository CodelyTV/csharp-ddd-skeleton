using System.Linq;
using System.Threading.Tasks;
using CodelyTv.Backoffice.Courses.Domain;

namespace CodelyTv.Backoffice.Courses.Application.SearchAll
{
    public class AllBackofficeCoursesSearcher
    {
        private readonly IBackofficeCourseRepository _repository;

        public AllBackofficeCoursesSearcher(IBackofficeCourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<BackofficeCoursesResponse> Search()
        {
            return new BackofficeCoursesResponse(
                (await _repository.SearchAll()).Select(BackofficeCourseResponse.FromAggregate));
        }
    }
}

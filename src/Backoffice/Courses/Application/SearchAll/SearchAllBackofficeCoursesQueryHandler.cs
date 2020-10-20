using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Query;

namespace CodelyTv.Backoffice.Courses.Application.SearchAll
{
    public class
        SearchAllBackofficeCoursesQueryHandler : QueryHandler<SearchAllBackofficeCoursesQuery,
            BackofficeCoursesResponse>
    {
        private readonly AllBackofficeCoursesSearcher _searcher;

        public SearchAllBackofficeCoursesQueryHandler(AllBackofficeCoursesSearcher searcher)
        {
            _searcher = searcher;
        }

        public async Task<BackofficeCoursesResponse> Handle(SearchAllBackofficeCoursesQuery query)
        {
            return await _searcher.Search();
        }
    }
}

using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Query;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Backoffice.Courses.Application.SearchByCriteria
{
    public class
        SearchBackofficeCoursesByCriteriaQueryHandler : QueryHandler<SearchBackofficeCoursesByCriteriaQuery,
            BackofficeCoursesResponse>
    {
        private readonly BackofficeCoursesByCriteriaSearcher _searcher;

        public SearchBackofficeCoursesByCriteriaQueryHandler(BackofficeCoursesByCriteriaSearcher searcher)
        {
            _searcher = searcher;
        }

        public async Task<BackofficeCoursesResponse> Handle(SearchBackofficeCoursesByCriteriaQuery query)
        {
            var filters = Filters.FromValues(query.Filters);
            var order = Order.FromValues(query.OrderBy, query.OrderType);

            return await _searcher.Search(filters, order, query.Limit, query.Offset);
        }
    }
}

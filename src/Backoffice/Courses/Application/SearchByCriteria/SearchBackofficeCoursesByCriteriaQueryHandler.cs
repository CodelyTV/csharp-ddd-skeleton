using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Query;
using CodelyTv.Shared.Domain.FiltersByCriteria;

namespace CodelyTv.Backoffice.Courses.Application.SearchByCriteria
{
    public class SearchBackofficeCoursesByCriteriaQueryHandler : QueryHandler<SearchBackofficeCoursesByCriteriaQuery, BackofficeCoursesResponse>
    {
        private readonly BackofficeCoursesByCriteriaSearcher _searcher;

        public SearchBackofficeCoursesByCriteriaQueryHandler(BackofficeCoursesByCriteriaSearcher searcher)
        {
            _searcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
        }

        public async Task<BackofficeCoursesResponse> Handle(SearchBackofficeCoursesByCriteriaQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            Filters filters = Filters.FromValues(query.Filters) ?? throw new InvalidOperationException("Filters are missing");
            var order = Order.FromValues(query.OrderBy ?? throw new InvalidOperationException("OrderBy is missing"), query.OrderType ?? throw new InvalidOperationException("OrderType is missing"));

            return await _searcher.Search(filters, order, query.Limit, query.Offset);
        }
    }
}

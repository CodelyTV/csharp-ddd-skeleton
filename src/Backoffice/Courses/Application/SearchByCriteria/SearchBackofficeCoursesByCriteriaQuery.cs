using System.Collections.Generic;
using CodelyTv.Shared.Domain.Bus.Query;

namespace CodelyTv.Backoffice.Courses.Application.SearchByCriteria
{
    public class SearchBackofficeCoursesByCriteriaQuery : Query
    {
        public List<Dictionary<string, string>> Filters { get; }
        public string OrderBy { get; }
        public string OrderType { get; }
        public int? Limit { get; }
        public int? Offset { get; }

        public SearchBackofficeCoursesByCriteriaQuery(List<Dictionary<string, string>> filters, string orderBy,
            string orderType, int? limit, int? offset)
        {
            Filters = filters;
            OrderBy = orderBy;
            OrderType = orderType;
            Limit = limit;
            Offset = offset;
        }
    }
}

using System;

namespace CodelyTv.Shared.Domain.Bus.Query
{
    public class QueryNotRegisteredError : Exception
    {
        public QueryNotRegisteredError(Query query) : base(
            $"The query {query} has not a query handler associated")
        {
        }
    }
}

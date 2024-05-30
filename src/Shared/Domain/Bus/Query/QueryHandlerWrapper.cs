using System;
using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Query
{
    internal abstract class QueryHandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> Handle(Query query, IServiceProvider provider);
    }

    internal class QueryHandlerWrapper<TQuery, TResponse> : QueryHandlerWrapper<TResponse>
        where TQuery : Query
    {
        public override async Task<TResponse> Handle(Query query, IServiceProvider provider)
        {
            var service = provider.GetService(typeof(QueryHandler<TQuery, TResponse>));
            if (!(service is QueryHandler<TQuery, TResponse> handler))
            {
                throw new SystemException("Query is not a valid query");
            }

            return await handler.Handle((TQuery)query);
        }
    }
}

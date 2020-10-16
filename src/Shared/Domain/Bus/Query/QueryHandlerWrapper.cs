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
            var handler =
                (IQueryHandler<TQuery, TResponse>) provider.GetService(typeof(IQueryHandler<TQuery, TResponse>));

            return await handler.Handle((TQuery) query);
        }
    }
}

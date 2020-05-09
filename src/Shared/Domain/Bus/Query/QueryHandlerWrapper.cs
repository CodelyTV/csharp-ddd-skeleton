namespace CodelyTv.Shared.Domain.Bus.Query
{
    using System.Threading.Tasks;

    internal abstract class QueryHandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> Handle(Query query);
    }

    internal class QueryHandlerWrapper<TQuery, TResponse> : QueryHandlerWrapper<TResponse>
        where TQuery : Query
    {
        private readonly IQueryHandler<TQuery,TResponse>  _handler;

        public QueryHandlerWrapper(IQueryHandler<TQuery, TResponse> handler)
        {
            _handler = handler;
        }
        
        public override Task<TResponse> Handle(Query query)
        {
            return _handler.Handle((TQuery) query);
        }
    }
}
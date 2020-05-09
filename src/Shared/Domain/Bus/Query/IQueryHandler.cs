namespace CodelyTv.Shared.Domain.Bus.Query
{
    using System.Threading.Tasks;

    public interface IQueryHandler<TQuery, TResponse> where TQuery : Query
    {
        Task<TResponse> Handle(TQuery query);
    }
}
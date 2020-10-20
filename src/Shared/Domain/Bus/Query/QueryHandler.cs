using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Query
{
    public interface QueryHandler<TQuery, TResponse> where TQuery : Query
    {
        Task<TResponse> Handle(TQuery query);
    }
}

using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Query
{
    public interface IQueryBus
    {
        Task<TResponse> Ask<TResponse>(Query request);
    }
}

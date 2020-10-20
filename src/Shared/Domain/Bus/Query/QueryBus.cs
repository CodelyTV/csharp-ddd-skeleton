using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Query
{
    public interface QueryBus
    {
        Task<TResponse> Ask<TResponse>(Query request);
    }
}

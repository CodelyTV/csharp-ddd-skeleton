namespace CodelyTv.Shared.Domain.Bus.Query
{
    using System.Threading.Tasks;

    public interface IQueryBus
    {
        Task<TResponse> Ask<TResponse>(Query request);
    }
}
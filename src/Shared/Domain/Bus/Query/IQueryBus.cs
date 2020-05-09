namespace CodelyTv.Shared.Domain.Bus.Query
{
    using System.Threading.Tasks;

    public interface IQueryBus
    {
        Task<TResponse> Send<TResponse>(Query request);
    }
}
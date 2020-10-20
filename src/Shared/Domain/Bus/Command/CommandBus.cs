using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Command
{
    public interface CommandBus
    {
        Task Dispatch(Command command);
    }
}

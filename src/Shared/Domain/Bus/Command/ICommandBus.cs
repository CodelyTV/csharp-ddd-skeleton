using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Command
{
    public interface ICommandBus
    {
        Task Dispatch(Command command);
    }
}

namespace CodelyTv.Shared.Domain.Bus.Command
{
    using System.Threading.Tasks;

    public interface ICommandBus
    {
        Task Dispatch(Command command);
    }
}
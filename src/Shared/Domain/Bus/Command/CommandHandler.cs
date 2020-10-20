using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Command
{
    public interface CommandHandler<TCommand> where TCommand : Command
    {
        Task Handle(TCommand command);
    }
}

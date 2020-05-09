namespace CodelyTv.Shared.Domain.Bus.Command
{
    using System.Threading.Tasks;

    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task Handle(TCommand command);
    }
}
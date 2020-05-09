namespace CodelyTv.Shared.Domain.Bus.Command
{
    using System.Threading.Tasks;

    internal abstract class CommandHandlerWrapper
    {
        public abstract Task Handle(Command command);
    }

    internal class CommandHandlerWrapper<TCommand> : CommandHandlerWrapper
        where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _handler;

        public CommandHandlerWrapper(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public override Task Handle(Command domainEvent)
        {
            return _handler.Handle((TCommand) domainEvent);
        }
    }
}
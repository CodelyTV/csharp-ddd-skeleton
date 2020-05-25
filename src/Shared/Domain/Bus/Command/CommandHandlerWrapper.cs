namespace CodelyTv.Shared.Domain.Bus.Command
{
    using System;
    using System.Threading.Tasks;

    internal abstract class CommandHandlerWrapper
    {
        public abstract Task Handle(Command command, IServiceProvider provider);
    }

    internal class CommandHandlerWrapper<TCommand> : CommandHandlerWrapper
        where TCommand : Command
    {
        public override async Task Handle(Command domainEvent, IServiceProvider provider)
        {
            var handler = (ICommandHandler<TCommand>) provider.GetService(typeof(ICommandHandler<TCommand>));
            await handler.Handle((TCommand) domainEvent);
        }
    }
}
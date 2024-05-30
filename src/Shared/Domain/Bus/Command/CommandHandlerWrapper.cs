using System;
using System.Threading.Tasks;

namespace CodelyTv.Shared.Domain.Bus.Command
{
    internal abstract class CommandHandlerWrapper
    {
        public abstract Task Handle(Command command, IServiceProvider provider);
    }

    internal class CommandHandlerWrapper<TCommand> : CommandHandlerWrapper
        where TCommand : Command
    {
        public override async Task Handle(Command command, IServiceProvider provider)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            var handler = provider.GetService(typeof(CommandHandler<TCommand>)) as CommandHandler<TCommand>;
            if (handler == null)
            {
                throw new InvalidOperationException($"Handler for {typeof(TCommand).Name} not found");
            }

            await handler.Handle((TCommand)command);
        }
    }
}

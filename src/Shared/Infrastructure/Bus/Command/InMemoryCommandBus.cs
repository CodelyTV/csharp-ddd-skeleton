using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Command;

namespace CodelyTv.Shared.Infrastructure.Bus.Command
{
    public class InMemoryCommandBus : CommandBus
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<CommandHandlerWrapper>> _commandHandlers =
            new ConcurrentDictionary<Type, IEnumerable<CommandHandlerWrapper>>();

        private readonly IServiceProvider _provider;

        public InMemoryCommandBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Dispatch(Domain.Bus.Command.Command command)
        {
            var wrappedHandlers = GetWrappedHandlers(command);

            if (wrappedHandlers == null) throw new CommandNotRegisteredError(command);

            foreach (var handler in wrappedHandlers) await handler.Handle(command, _provider);
        }

        private IEnumerable<CommandHandlerWrapper> GetWrappedHandlers(Domain.Bus.Command.Command command)
        {
            var handlerType = typeof(CommandHandler<>).MakeGenericType(command.GetType());
            var wrapperType = typeof(CommandHandlerWrapper<>).MakeGenericType(command.GetType());

            var handlers =
                (IEnumerable) _provider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType));

            var wrappedHandlers = _commandHandlers.GetOrAdd(command.GetType(), handlers.Cast<object>()
                .Select(handler => (CommandHandlerWrapper) Activator.CreateInstance(wrapperType)));

            return wrappedHandlers;
        }
    }
}

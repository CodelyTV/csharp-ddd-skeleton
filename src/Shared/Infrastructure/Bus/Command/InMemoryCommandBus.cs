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
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task Dispatch(Domain.Bus.Command.Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var wrappedHandlers = GetWrappedHandlers(command);

            if (wrappedHandlers == null || !wrappedHandlers.Any())
            {
                throw new CommandNotRegisteredError(command);
            }

            foreach (var handler in wrappedHandlers)
            {
                if (handler == null)
                {
                    continue;
                }

                await handler.Handle(command, _provider);
            }
        }

        private IEnumerable<CommandHandlerWrapper> GetWrappedHandlers(Domain.Bus.Command.Command command)
        {
            var handlerType = typeof(CommandHandler<>).MakeGenericType(command.GetType());
            var wrapperType = typeof(CommandHandlerWrapper<>).MakeGenericType(command.GetType());

            var handlers = _provider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType)) as IEnumerable<object>;
            if (handlers == null)
            {
                throw new InvalidOperationException($"Handlers for command type {command.GetType().Name} not found");
            }

            var wrappedHandlers = _commandHandlers.GetOrAdd(command.GetType(), _ => handlers
                .Select(handler => (CommandHandlerWrapper)(Activator.CreateInstance(wrapperType) ?? throw new InvalidOperationException($"Unable to create instance of {wrapperType.Name}")))
                .ToList());

            return wrappedHandlers;
        }
    }
}

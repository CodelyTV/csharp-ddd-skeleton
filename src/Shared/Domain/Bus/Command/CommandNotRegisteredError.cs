using System;

namespace CodelyTv.Shared.Domain.Bus.Command
{
    public class CommandNotRegisteredError : Exception
    {
        public CommandNotRegisteredError(Command command) : base(
            $"The command {command} has not a command handler associated")
        {
        }
    }
}

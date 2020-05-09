namespace CodelyTv.Shared.Domain.Bus.Command
{
    using System;

    public class CommandNotRegisteredError : Exception
    {
        public CommandNotRegisteredError(Command command) : base(
            $"The command {command} has not a command handler associated")
        {
        }
    }
}
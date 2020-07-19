namespace CodelyTv.Shared.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class CommandBuilder<T>
    {
        protected ServiceProvider Provider { get; set; }
        private readonly string[] _args;
        private readonly Dictionary<string, Type> Commands;

        protected CommandBuilder(string[] args, Dictionary<string, Type> commands)
        {
            _args = args;
            Commands = commands;
        }
        public abstract T Build(IConfigurationRoot config);

        public virtual void Run()
        {
            var command = GetCommands();

            using IServiceScope scope = Provider.CreateScope();

            Type commandType = command;
            object service = scope.ServiceProvider.GetService(commandType);
            ((Command) service).Execute(_args);
        }

        protected Type GetCommands()
        {
            var command = Commands.FirstOrDefault(cmd => _args.Contains(cmd.Key));
            if (command.Value == null) throw new SystemException("arguments does not match with any command");

            return command.Value;
        }
    }
}
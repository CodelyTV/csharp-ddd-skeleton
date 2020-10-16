using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Shared.Cli
{
    public abstract class CommandBuilder<T>
    {
        private readonly string[] _args;
        private readonly Dictionary<string, Type> Commands;

        protected ServiceProvider Provider { get; set; }

        protected CommandBuilder(string[] args, Dictionary<string, Type> commands)
        {
            _args = args;
            Commands = commands;
        }

        public abstract T Build(IConfigurationRoot config);

        public virtual void Run()
        {
            var command = GetCommand();

            using var scope = Provider.CreateScope();

            var service = scope.ServiceProvider.GetService(command);
            ((Command) service).Execute(_args);
        }

        protected Type GetCommand()
        {
            var command = Commands.FirstOrDefault(cmd => _args.Contains(cmd.Key));
            if (command.Value == null) throw new SystemException("arguments does not match with any command");

            return command.Value;
        }
    }
}

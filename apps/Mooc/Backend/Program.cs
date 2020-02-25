namespace CodelyTv.Apps.Mooc.Backend
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Command;
    using Extension.DependencyInjection;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Domain.Bus.Event;

    public static class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any()) CreateWebHostBuilder(args).Build().Run();

            var serviceProvider = ServicesProvider();
            var command = Commands().FirstOrDefault(cmd => args.Contains(cmd.Key));

            if (command.Key == null) throw new SystemException("arguments does not match with any command");

            ExecuteCommand(args, command, serviceProvider);
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }

        private static ServiceProvider ServicesProvider()
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure(Configuration());

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ExecuteCommand(string[] args, KeyValuePair<string, Type> command,
            ServiceProvider serviceProvider)
        {
            Type commandType = command.Value;
            var instance = Activator.CreateInstance(commandType, serviceProvider.GetService<IDomainEventsConsumer>());

            commandType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(Shared.Cli.Command.Execute))
                .Invoke(instance, new object[]
                {
                    args
                });
        }

        private static Dictionary<string, Type> Commands()
        {
            return new Dictionary<string, Type>()
            {
                {"domain-events:mysql:consume", typeof(ConsumeMsSqlDomainEventsCommand)},
                {"domain-events:rabbitmq:consume", typeof(ConsumeRabbitMqDomainEventsCommand)}
            };
        }

        private static IConfigurationRoot Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
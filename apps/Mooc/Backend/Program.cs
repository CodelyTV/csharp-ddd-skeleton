namespace CodelyTv.Apps.Mooc.Backend
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Command;
    using Extension.DependencyInjection;
    using Helper;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Shared;

    public static class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any()) CreateWebHostBuilder(args).Build().Run();

            var serviceProvider = CommandServicesProvider();
            var command = Commands().FirstOrDefault(cmd => args.Contains(cmd.Key));

            if (command.Key == null) throw new SystemException("arguments does not match with any command");

            ExecuteCommand(args, command, serviceProvider);
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }

        private static ServiceProvider CommandServicesProvider()
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure(Configuration());
            services.AddDomainEventSubscriberInformationService(AssemblyHelper.Instance());

            services.AddScoped<ConsumeRabbitMqDomainEventsCommand, ConsumeRabbitMqDomainEventsCommand>();
            services.AddScoped<ConsumeMsSqlDomainEventsCommand, ConsumeMsSqlDomainEventsCommand>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ExecuteCommand(string[] args, KeyValuePair<string, Type> command,
            ServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            Type commandType = command.Value;
            object service = scope.ServiceProvider.GetService(commandType);
            ((Shared.Cli.Command) service).Execute(args);
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
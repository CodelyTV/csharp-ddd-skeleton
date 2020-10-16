using System;
using System.Collections.Generic;
using CodelyTv.Apps.Mooc.Backend.Extension.DependencyInjection;
using CodelyTv.Shared;
using CodelyTv.Shared.Cli;
using CodelyTv.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Apps.Mooc.Backend.Command
{
    public class MoocBackendCommandBuilder : CommandBuilder<MoocBackendCommandBuilder>
    {
        public MoocBackendCommandBuilder(string[] args) : base(args,
            new Dictionary<string, Type>
            {
                {"domain-events:mysql:consume", typeof(ConsumeMsSqlDomainEventsCommand)},
                {"domain-events:rabbitmq:consume", typeof(ConsumeRabbitMqDomainEventsCommand)}
            })
        {
        }

        public static MoocBackendCommandBuilder Create(string[] args)
        {
            return new MoocBackendCommandBuilder(args);
        }

        public override MoocBackendCommandBuilder Build(IConfigurationRoot config)
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure(config);
            services.AddDomainEventSubscriberInformationService(AssemblyHelper.GetInstance(Assemblies.Mooc));

            services.AddScoped<ConsumeRabbitMqDomainEventsCommand, ConsumeRabbitMqDomainEventsCommand>();
            services.AddScoped<ConsumeMsSqlDomainEventsCommand, ConsumeMsSqlDomainEventsCommand>();

            Provider = services.BuildServiceProvider();
            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using CodelyTv.Apps.Backoffice.Frontend.Extension.DependencyInjection;
using CodelyTv.Shared;
using CodelyTv.Shared.Cli;
using CodelyTv.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodelyTv.Apps.Backoffice.Frontend.Command
{
    public class BackofficeFrontendCommandBuilder : CommandBuilder<BackofficeFrontendCommandBuilder>
    {
        protected BackofficeFrontendCommandBuilder(string[] args) : base(args,
            new Dictionary<string, Type>
            {
                {"domain-events:rabbitmq:consume", typeof(ConsumeRabbitMqDomainEventsCommand)}
            })
        {
        }

        public static BackofficeFrontendCommandBuilder Create(string[] args)
        {
            return new BackofficeFrontendCommandBuilder(args);
        }

        public override BackofficeFrontendCommandBuilder Build(IConfigurationRoot config)
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure(config);
            services.AddDomainEventSubscriberInformationService(AssemblyHelper.GetInstance(Assemblies.Mooc));
            services.AddDomainEventSubscriberInformationService(AssemblyHelper.GetInstance(Assemblies.Backoffice));

            services.AddScoped<ConsumeRabbitMqDomainEventsCommand, ConsumeRabbitMqDomainEventsCommand>();

            Provider = services.BuildServiceProvider();
            return this;
        }
    }
}

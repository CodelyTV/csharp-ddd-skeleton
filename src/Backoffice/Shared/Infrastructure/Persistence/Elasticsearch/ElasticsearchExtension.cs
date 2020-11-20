using System;
using CodelyTv.Backoffice.Courses.Domain;
using CodelyTv.Backoffice.Courses.Infrastructure.Persistence.Elasticsearch;
using CodelyTv.Shared.Infrastructure.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace CodelyTv.Backoffice.Shared.Infrastructure.Persistence.Elasticsearch
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultIndex = configuration["Elasticsearch:IndexPrefix"];
            var url = $"{configuration["Elasticsearch:Host"]}:{configuration["Elasticsearch:Port"]}";

            var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(defaultIndex);

            var client = new ElasticClient(settings);
            var elastic = new ElasticsearchClient(client, defaultIndex);

            GenerateIndexIfNotExist(elastic.IndexFor(nameof(BackofficeCourse)), elastic);

            services.AddSingleton(client);
            services.AddSingleton(elastic);
        }

        private static void GenerateIndexIfNotExist(string defaultIndex, ElasticsearchClient client)
        {
            var elasticClient = client.Client;

            if (!elasticClient.Indices.Exists(defaultIndex).Exists)
            {
                var descriptor = new CreateIndexDescriptor(defaultIndex)
                    .CreateBackofficeCourseDescriptor();

                var createIndexResponse = elasticClient.Indices.Create(descriptor);
                if (!createIndexResponse.IsValid)
                    throw new Exception(createIndexResponse.DebugInformation);
            }
        }
    }
}

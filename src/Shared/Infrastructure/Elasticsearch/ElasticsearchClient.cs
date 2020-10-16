using System.Threading.Tasks;
using Nest;

namespace CodelyTv.Shared.Infrastructure.Elasticsearch
{
    public class ElasticsearchClient
    {
        private readonly string IndexPrefix;

        public ElasticClient Client { get; }

        public ElasticsearchClient(ElasticClient client, string indexPrefix)
        {
            Client = client;
            IndexPrefix = indexPrefix;
        }

        public async Task Persist(string index, string id, string json)
        {
            await Client.LowLevel.IndexAsync<IndexResponse>(index, id, json);
        }

        public string IndexFor(string moduleName)
        {
            return $"{IndexPrefix}_{moduleName}".ToLowerInvariant();
        }
    }
}

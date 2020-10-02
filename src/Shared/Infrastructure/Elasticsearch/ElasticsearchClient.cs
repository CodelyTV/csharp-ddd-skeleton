namespace CodelyTv.Shared.Infrastructure.Elasticsearch
{
    using System.Threading.Tasks;
    using Nest;

    public class ElasticsearchClient
    {
        public ElasticClient Client { get; }
        private readonly string IndexPrefix;

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
            return $"{IndexPrefix}_{moduleName}".ToLower();
        }
    }
}
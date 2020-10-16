using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.FiltersByCriteria;
using Nest;

namespace CodelyTv.Shared.Infrastructure.Elasticsearch
{
    public abstract class ElasticsearchRepository<T> where T : class
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticsearchCriteriaConverter<T> _criteriaConverter;

        public ElasticsearchRepository(ElasticsearchClient client)
        {
            _client = client;
            _criteriaConverter = new ElasticsearchCriteriaConverter<T>();
        }

        protected abstract string ModuleName();

        protected async Task<IReadOnlyCollection<Dictionary<string, object>>> SearchAllInElastic()
        {
            var searchDescriptor = new SearchDescriptor<T>();
            searchDescriptor.MatchAll();
            searchDescriptor.Index(_client.IndexFor(ModuleName()));

            return (await _client.Client.SearchAsync<Dictionary<string, object>>(searchDescriptor))?.Documents;
        }

        protected async Task<IReadOnlyCollection<Dictionary<string, object>>> SearchByCriteria(Criteria criteria)
        {
            var searchDescriptor = _criteriaConverter.Convert(criteria, _client.IndexFor(ModuleName()));

            return (await _client.Client.SearchAsync<Dictionary<string, object>>(searchDescriptor))?.Documents;
        }

        protected async Task Persist(string id, string json)
        {
            await _client.Persist(_client.IndexFor(ModuleName()), id, json);
        }
    }
}

using Nest;

namespace CodelyTv.Test.Backoffice.Shared.Infrastructure.Persistence
{
    public class ElasticDatabaseCleaner
    {
        private ElasticClient _client;

        public ElasticDatabaseCleaner(ElasticClient client)
        {
            _client = client;
        }

        public void Execute()
        {
            var request = _client.Indices.Get(new GetIndexRequest(Indices.All));

            foreach (var index in request.Indices)
            {
                _client.Indices.Delete(index.Key);
            }
        }
    }
}

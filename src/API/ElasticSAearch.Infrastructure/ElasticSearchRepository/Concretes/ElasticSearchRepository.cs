using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ElasticSAearch.Entities.Entities;
using ElasticSAearch.Infrastructure.ElasticSearchRepository.Contracts;
using Microsoft.Extensions.Options;

namespace ElasticSAearch.Infrastructure.ElasticSearchRepository.Concretes;

public class ElasticSearchRepository : IElasticSearchRepository
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _elasticsearchSettings;

    public ElasticSearchRepository(IOptions<ElasticSettings> optionsMonitor)
    {
        _elasticsearchSettings = optionsMonitor.Value;
        var settings = new ElasticsearchClientSettings(new Uri(_elasticsearchSettings.Url)).DefaultIndex(_elasticsearchSettings.DefaultIndex);
        _client = new ElasticsearchClient(settings);
    }

    public async Task CreateIndexIfNotExistAsync(string indexName)
    {
       if(!_client.Indices.Exists(indexName).Exists)
            await _client.Indices.CreateAsync(indexName);
    }

    public async Task<bool> AddOrUpdateAsync(UserEntity user)
    {
        var response = await _client.IndexAsync(user, idx => idx.Index(_elasticsearchSettings.DefaultIndex).OpType(OpType.Index));
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulkAsync(IEnumerable<UserEntity> users, string indexName)
    {
        var response = await _client.BulkAsync(b => b.Index(_elasticsearchSettings.DefaultIndex).UpdateMany(users, (ud, u) => ud.Doc(u).DocAsUpsert(true)));
        return response.IsValidResponse;
    }

    public async Task<UserEntity> Get(string Key)
    {
        var response = await _client.GetAsync<UserEntity>(Key, u => u.Index(_elasticsearchSettings.DefaultIndex));
        return response.Source;
    }

    public async Task<List<UserEntity>> GetAll(string Key)
    {
        var response = await _client.SearchAsync<UserEntity>(s => s.Index(_elasticsearchSettings.DefaultIndex));
        return response.IsValidResponse ? response.Documents.ToList() : default;
    }

    public async Task<bool> Remove(string Key)
    {
        var response = await _client.DeleteAsync<UserEntity>(Key, d => d.Index(_elasticsearchSettings.DefaultIndex));
        return response.IsValidResponse;
    }

    public async Task<long?> RemoveAll(string Key)
    {
        var response = await _client.DeleteByQueryAsync<UserEntity>(d => d.Indices(_elasticsearchSettings.DefaultIndex));
        return response.IsValidResponse ? response.Deleted : default;
    }
}

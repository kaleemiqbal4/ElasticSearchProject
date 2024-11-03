using ElasticSAearch.Entities.Entities;

namespace ElasticSAearch.Infrastructure.ElasticSearchRepository.Contracts;

public interface IElasticSearchRepository
{
    Task CreateIndexIfNotExistAsync(string indexName);

    Task<bool> AddOrUpdateAsync(UserEntity user);

    Task<bool> AddOrUpdateBulkAsync(IEnumerable<UserEntity> users, string indexName);

    Task<UserEntity> Get(string Key);

    Task<List<UserEntity>> GetAll(string Key);

    Task<bool> Remove(string Key);

    Task<long?> RemoveAll(string Key);
}

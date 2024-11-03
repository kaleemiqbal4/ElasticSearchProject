using ElasticSAearch.Entities.Entities;
using ElasticSAearch.Infrastructure.Repositories.Contracts;

namespace ElasticSAearch.Infrastructure.Repositories.Concretes;

public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<UserEntity>(dbContext), IUserRepository
{ 
}

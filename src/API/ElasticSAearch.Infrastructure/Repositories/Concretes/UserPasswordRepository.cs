using ElasticSAearch.Entities.Entities;
using ElasticSAearch.Infrastructure.Repositories.Contracts;

namespace ElasticSAearch.Infrastructure.Repositories.Concretes;

public class UserPasswordRepository(ApplicationDbContext dbContext) : BaseRepository<UserPasswordEntity>(dbContext), IUserPasswordRepository
{
}

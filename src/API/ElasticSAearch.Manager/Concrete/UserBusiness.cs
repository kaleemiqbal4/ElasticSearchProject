using AutoMapper;
using ElasticSAearch.Entities.Entities;
using ElasticSAearch.Infrastructure.ElasticSearchRepository.Concretes;
using ElasticSAearch.Infrastructure.ElasticSearchRepository.Contracts;
using ElasticSAearch.Infrastructure.Repositories.Contracts;
using ElasticSAearch.Infrastructure.UOW;
using ElasticSAearch.Manager.Contract;
using ElasticSAearch.Models.RequestModels;
using ElasticSAearch.Models.Response;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ElasticSAearch.Manager.Concrete;

public class UserBusiness : IUserBusiness
{
    private readonly ILogger<UserBusiness> logger;
    private readonly IUserPasswordBusiness passwordBusiness;
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork wrapper;
    private readonly IMapper mapper;
    private readonly IElasticSearchRepository elasticSearchRepository;

    /// <summary></summary>
    /// <param name="_logger"></param>
    /// <param name="_passwordBusiness"></param>
    /// <param name="_userRepository"></param>
    /// <param name="_wrapper"></param>
    /// <param name="_mapper"></param>
    /// <param name="_elasticSearchRepository"></param>
    public UserBusiness(ILogger<UserBusiness> _logger, IUserPasswordBusiness _passwordBusiness, IUserRepository _userRepository, IUnitOfWork _wrapper, IMapper _mapper, IElasticSearchRepository _elasticSearchRepository) =>
        (logger, passwordBusiness, userRepository, wrapper, mapper, elasticSearchRepository) = (_logger, _passwordBusiness, _userRepository, _wrapper, _mapper, _elasticSearchRepository);

    /// <summary></summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BusinessResponse> CreateUserAsync(UserModel userModel, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<UserEntity>(userModel);
        await elasticSearchRepository.CreateIndexIfNotExistAsync("users");
        await userRepository.CreateAsync(entity);
        var userPasswordModel = new UserPasswordModel()
        {
            UserId = entity.Id,
            Password = userModel.Password
        };
        var result = await elasticSearchRepository.AddOrUpdateAsync(entity);
        await passwordBusiness.CreateUserPasswordAsync(userPasswordModel, cancellationToken);
        return await wrapper.SaveChangesAsync(cancellationToken) > 0
        ? new BusinessResponse
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Resourse has been created",
            Data = new { entity.Id, entity.Name, entity.Email }
        } : new BusinessResponse
        {
            StatusCode = (int)HttpStatusCode.Conflict,
            Message = "Resourse has not been created",
            Data = null
        };
    }
}

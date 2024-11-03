using AutoMapper;
using ElasticSAearch.Common;
using ElasticSAearch.Entities.Entities;
using ElasticSAearch.Infrastructure.Repositories.Contracts;
using ElasticSAearch.Infrastructure.UOW;
using ElasticSAearch.Manager.Contract;
using ElasticSAearch.Models.RequestModels;
using ElasticSAearch.Models.Response;
using Microsoft.Extensions.Logging;

namespace ElasticSAearch.Manager.Concrete;

public class UserPasswordBusiness : IUserPasswordBusiness
{
    private readonly ILogger<UserBusiness> logger;
    private readonly IUserPasswordRepository userPasswordRepository;
    private readonly IUnitOfWork wrapper;
    private readonly IMapper mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_logger"></param>
    /// <param name="_userPasswordRepository"></param>
    /// <param name="_wrapper"></param>
    /// <param name="_mapper"></param>
    public UserPasswordBusiness(ILogger<UserBusiness> _logger, IUserPasswordRepository _userPasswordRepository, IUnitOfWork _wrapper, IMapper _mapper) => 
        (logger, userPasswordRepository, wrapper, mapper) = (_logger, _userPasswordRepository, _wrapper, _mapper);

    /// <summary></summary>
    /// <param name="userPasswordModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BusinessResponse> CreateUserPasswordAsync(UserPasswordModel userPasswordModel, CancellationToken cancellationToken)
    {
        var hashSalt = userPasswordModel.Password.HashPassword();
        var entity = new UserPasswordEntity()
        {
            UserId = userPasswordModel.UserId,
            Salt = hashSalt.Salt,
            Hash = hashSalt.Hash
        };
        await userPasswordRepository.CreateAsync(entity);
        return new BusinessResponse { };
    }

}

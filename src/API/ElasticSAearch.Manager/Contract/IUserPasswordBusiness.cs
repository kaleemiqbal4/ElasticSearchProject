using ElasticSAearch.Models.RequestModels;
using ElasticSAearch.Models.Response;

namespace ElasticSAearch.Manager.Contract;

public interface IUserPasswordBusiness
{
    /// <summary></summary>
    /// <param name="userPasswordModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BusinessResponse> CreateUserPasswordAsync(UserPasswordModel userPasswordModel, CancellationToken cancellationToken);
}

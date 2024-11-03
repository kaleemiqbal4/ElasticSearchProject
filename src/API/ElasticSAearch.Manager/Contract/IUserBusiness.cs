using ElasticSAearch.Models.RequestModels;
using ElasticSAearch.Models.Response;

namespace ElasticSAearch.Manager.Contract;

public interface IUserBusiness
{
    /// <summary></summary>
    /// <param name="userModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BusinessResponse> CreateUserAsync(UserModel userModel, CancellationToken cancellationToken);
}

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Security;
using ElasticSAearch.Manager.Contract;
using ElasticSAearch.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSAearch.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly IUserBusiness userBusiness;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_logger"></param>
    /// <param name="_userBusiness"></param>
    public UsersController(ILogger<UsersController> _logger, IUserBusiness _userBusiness) => 
        (logger, userBusiness) = (_logger, _userBusiness);

    /// <summary> </summary>
    /// <param name="userModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SaveUserAsync([FromBody] UserModel userModel, CancellationToken cancellationToken)
    {
        var result = await userBusiness.CreateUserAsync(userModel, cancellationToken);
        return result.StatusCode is 201 ? Ok(result) : BadRequest(result);
    }
}

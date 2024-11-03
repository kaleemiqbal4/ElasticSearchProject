using ElasticSAearch.Infrastructure.ElasticSearchRepository.Concretes;
using ElasticSAearch.Infrastructure.ElasticSearchRepository.Contracts;
using ElasticSAearch.Infrastructure.Repositories.Concretes;
using ElasticSAearch.Infrastructure.Repositories.Contracts;
using ElasticSAearch.Infrastructure.UOW;
using ElasticSAearch.Manager.Concrete;
using ElasticSAearch.Manager.Contract;

namespace ElasticSAearch.API.Extensions;

/// <summary> </summary>
public static class RegisterDependency
{
    /// <summary></summary>
    /// <param name="builder"></param>
    public static void ConfigureDependency(this WebApplicationBuilder builder)
    {
        #region "Register Repository"
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region "Register Services"
        builder.Services.AddScoped<IUserBusiness, UserBusiness>();
        builder.Services.AddScoped<IUserPasswordBusiness, UserPasswordBusiness>();
        #endregion

        #region "Elastic Search"
        builder.Services.AddSingleton<IElasticSearchRepository, ElasticSearchRepository>();
        #endregion
    }
}

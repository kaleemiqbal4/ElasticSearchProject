using ElasticSAearch.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ElasticSAearch.API.Extension;
using ElasticSAearch.API.ModelValidation;
using Microsoft.AspNetCore.Mvc;
using ElasticSAearch.API.Middleware;
using ElasticSAearch.Manager.DTOMapperProfile;
using ElasticSAearch.API.Extensions;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using ElasticSAearch.Common;

namespace ElasticSAearch.API;

/// <summary></summary>
public static class ServiceDependencies
{
    /// <summary>Register services</summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
    {
        string conStr = string.Empty;
        builder.Services.Configure<KeyVaultConfiguration>(builder.Configuration.GetSection("KeyVaultSecrets"));
        builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));
        conStr = ReadKeyVault(builder).Result;
        #region register SqlServer Context
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(conStr));
        #endregion
       //  builder.SetElasticExtension();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        #region"AutoMapper"
        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        }, AppDomain.CurrentDomain.GetAssemblies());
        #endregion
        #region Model Validation"
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ModelErrorResponse.GenerateErrorResponse;
        });
        #endregion
        builder.ConfigureDependency();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.SetupSwagger();
        builder.ExtendCors();
        return builder;
    }

    /// <summary>Register http pipelines </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication ConfigurePipeLine(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors();
        return app;
    }

    static async Task<string> ReadKeyVault(WebApplicationBuilder builder)
    {
        var keyVaultSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<KeyVaultConfiguration>>().Value;
        var clientSecretCredential = new ClientSecretCredential(
        keyVaultSettings.TenantId,
        keyVaultSettings.ClientId,
        keyVaultSettings.ClientSecret);

        var client = new SecretClient(new Uri(keyVaultSettings.keyVaultUrl), clientSecretCredential);
        KeyVaultSecret secret = await client.GetSecretAsync(keyVaultSettings.SecretName);
        return secret.Value;
    }
}
using Elastic.Clients.Elasticsearch;

namespace ElasticSAearch.API.Extension;

/// <summary>
/// 
/// </summary>
public static class ElaticSearchExtension
{
    /// <summary></summary>
    /// <param name="builder"></param>
    public static void SetElasticExtension(this WebApplicationBuilder builder)
    {
        var elasticsearchOptions = builder.Configuration.GetSection("ElasticSettings");
        var elasticsearchUrl = elasticsearchOptions.GetValue<string>("Url");
        var defaultIndex = elasticsearchOptions.GetValue<string>("DefaultIndex");
        // Register the Elasticsearch client
        builder.Services.AddSingleton(new ElasticsearchClient(new Uri(elasticsearchUrl)));
    }
}

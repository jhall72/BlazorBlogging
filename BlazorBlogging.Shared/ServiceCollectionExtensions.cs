using BlazorBlogging.Shared.Data;
using BlazorBlogging.Shared.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BlazorBlogging.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorBlogging(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new BlazorBloggingOptions();
        configuration.Bind(options);

        var mongoClient = new MongoClient(options.MongoDB.ConnectionString);
        services.AddDbContext<BlogDbContext>(dbOptions =>
            dbOptions.UseMongoDB(mongoClient, options.MongoDB.DatabaseName));

        services.AddScoped<BlogService>();
        services.AddSingleton(new AiSuggestionService(options.Anthropic.ApiKey));

        return services;
    }
}

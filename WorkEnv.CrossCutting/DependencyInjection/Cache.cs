using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class Cache
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration config)
    {
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            var connectionString = config.GetConnectionString("Redis") ?? string.Empty;

            redisOptions.Configuration = connectionString;
        });
        
        return services;
    }
}
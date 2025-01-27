using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class DbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection")
                               ?? throw new NullReferenceException("PostGreSQL connection string is null");

        services.AddDbContext<WorkEnvDbContext>(options => 
                options.UseNpgsql(connectionString));

        return services;
    }
}
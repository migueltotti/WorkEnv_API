using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class DbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        var workEnvConnectionString = config.GetConnectionString("WorkEnvDataBase")
                               ?? throw new NullReferenceException("PostGreSQL WorkEnvDb connection string is null");

        services.AddDbContext<WorkEnvDbContext>(options =>
            options.UseNpgsql(workEnvConnectionString));

        return services;
    }
}
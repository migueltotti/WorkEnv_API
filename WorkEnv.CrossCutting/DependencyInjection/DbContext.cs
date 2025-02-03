using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Infrastructure.Context;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class DbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        var workEnvConnectionString = config.GetConnectionString("WorkEnvDataBase")
                               ?? throw new NullReferenceException("PostGreSQL WorkEnvDb connection string is null");
        
        var authConnectionString = config.GetConnectionString("AuthDataBase")
                                      ?? throw new NullReferenceException("PostGreSQL AuthDb connection string is null");

        services.AddDbContext<WorkEnvDbContext>(options =>
            options.UseNpgsql(workEnvConnectionString));
        
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(authConnectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
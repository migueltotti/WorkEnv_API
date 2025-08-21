using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Domain.Entities;
using WorkEnv.Infrastructure.Context;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class DbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        var workEnvConnectionString = Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING")
                               ?? throw new NullReferenceException("PostGreSQL WorkEnvDb connection string is null");

        services.AddDbContext<WorkEnvDbContext>(options =>
            options.UseNpgsql(workEnvConnectionString));
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<WorkEnvDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
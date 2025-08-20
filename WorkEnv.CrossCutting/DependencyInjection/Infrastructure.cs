using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Infrastructure.Context;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static partial class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddCORS();
        
        services.AddRateLimiter();
        
        services.AddDbContext();
        
        services.AddRepositories();

        services.AddTokenManager();

        services.AddSecurity();

        services.AddValidators();

        services.AddCQRS();

        services.AddAuthenticationServices(config);

        services.AddApplicationUserConfiguration();

        return services;
    }
}
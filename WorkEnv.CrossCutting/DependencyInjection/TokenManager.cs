using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Authentication;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class TokenManager
{
    public static IServiceCollection AddTokenManager(this IServiceCollection services)
    {
        services.AddScoped<ITokenManager, WorkEnv.Infrastructure.Authentication.TokenManager>();

        return services;
    }
}
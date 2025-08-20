using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Application.Services;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class Security
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddScoped<IEncryptService, EncryptService>();
        
        return services;
    }
}
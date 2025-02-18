using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static partial class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext(config);

        services.AddRepositories();

        services.AddTokenManager();

        services.AddValidators();

        services.AddCQRS();

        services.AddAuthenticationServices(config);

        return services;
    }
}
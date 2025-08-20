using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Application.Validation.Auth;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static partial class Infrastructure
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(LoginCommandValidator)));
        
        return services;
    }
}
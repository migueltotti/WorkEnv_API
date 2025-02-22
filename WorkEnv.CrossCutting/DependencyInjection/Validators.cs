using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static partial class Infrastructure
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssembly()
        
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class CQRS
{
    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        // services.AddMediatR(cfg => 
        //     cfg.RegisterServicesFromAssembly(typeof(Infrastructure).Assembly));
        
        return services;
    }
}
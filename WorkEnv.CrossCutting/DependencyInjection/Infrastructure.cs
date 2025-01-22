using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        // services.AddDbContext<>(options => 
        //     options.UseNpgsql(""))

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Infrastructure).Assembly));

        //services.AddValidatorsFromAssembly()

        return services;
    }
}
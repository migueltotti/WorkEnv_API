using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<WorkEnvDbContext>(options => 
            options.UseNpgsql(connectionString));

        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(Infrastructure).Assembly));

        //services.AddValidatorsFromAssembly()

        return services;
    }
}
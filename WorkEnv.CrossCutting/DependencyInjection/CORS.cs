using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class CORS
{
    public static IServiceCollection AddCORS(this IServiceCollection services)
    {
        services.AddCors( options =>
        {
            options.AddPolicy("EnableCors", police =>
            {
                // TODO: Update this before deploy.
                police.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build();
            });
        });
        
        return services;
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.Infrastructure.Identity;

public static class ApplicationUserConfiguration
{
    public static IServiceCollection AddApplicationUserConfiguration(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = 
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@áàâãäÁÀÂÃÄéèêëÉÈÊËíìîïÍÌÎÏóòôõöÓÒÔÕÖúùûüÚÙÛÜçÇñÑýÿÝŸœŒøØåÅæÆß\n";
        });

        return services;
    }
}
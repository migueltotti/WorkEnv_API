using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class Authentication
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
    {
        var secretKey = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET_KEY") ?? 
                        throw new NullReferenceException("Invalid Secret Key!");
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = config["JWT:Audience"],
                ValidIssuer = config["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey))
            };
        });
        
        return services;
    }
}
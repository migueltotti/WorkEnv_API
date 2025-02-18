using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Infrastructure.Authentication;

public class TokenManager : ITokenManager
{
    private readonly IConfiguration _config;

    public TokenManager(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateAccessTokenAsync(User user)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));
        
        var expirationTimeInMinutes = jwtSettings.GetValue<int>("ExpirationTimeInMinutes");
        
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.Name!),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationTimeInMinutes),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshTokenAsync()
    {
        var secureRandomBytes = new byte[128];
        
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(secureRandomBytes);
        
        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        
        return refreshToken;
    }
}
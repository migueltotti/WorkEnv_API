using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.Infrastructure.Authentication;

public class TokenManager : ITokenManager
{
    private readonly IConfiguration _config;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public TokenManager(IConfiguration config)
    {
        _config = config;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateAccessToken(ApplicationUser user)
    {
        var rawSecretKey = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET_KEY")
                           ?? throw new InvalidOperationException("Invalid secret key!");
        
        var jwtSettings = _config.GetSection("JWT");
        
        var secretKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(rawSecretKey ?? string.Empty));
        
        var expirationTimeInMinutes = jwtSettings.GetValue<int>("TokenValidityInMinutes");
        
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.GetFormatedUserName()!),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationTimeInMinutes),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[128];
        
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(secureRandomBytes);
        
        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        
        return refreshToken;
    }
    
    public ClaimsPrincipal GetClaimsFromExpiredToken(string token)
    {
        var secretKey = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET_KEY")
                        ?? throw new InvalidOperationException("Invalid secret key!");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = false
        };
        
        var claims = _tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        return claims;
    }
}
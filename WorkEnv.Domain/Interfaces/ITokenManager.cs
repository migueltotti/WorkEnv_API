using System.Security.Claims;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface ITokenManager
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsFromExpiredToken(string token);
}
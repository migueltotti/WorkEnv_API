using System.Security.Claims;
using WorkEnv.Domain.Entities;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.Infrastructure.Authentication;

public interface ITokenManager
{
    string GenerateAccessToken(ApplicationUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsFromExpiredToken(string token);
}
using System.Security.Claims;

namespace WorkEnv.Application.Services;

public static class UserIdClaimService
{
    public static Guid GetUserIdAsGuid(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirst("sub")?.Value!);
    }
}
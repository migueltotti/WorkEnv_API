using Microsoft.AspNetCore.Identity;

namespace WorkEnv.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }    
}
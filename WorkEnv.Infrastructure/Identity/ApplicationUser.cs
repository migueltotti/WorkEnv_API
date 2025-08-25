using Microsoft.AspNetCore.Identity;

namespace WorkEnv.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpireAt { get; set; }

    public ApplicationUser()
    {
    }

    public ApplicationUser(Guid userId, string name, string email)
    {
        this.Id = userId.ToString();
        this.UserName = GenerateFormatedUserName(name);
        this.Email = email;
        this.SecurityStamp = Guid.NewGuid().ToString();
    }
    
    public Guid GetUserIdAsGuid() => Guid.Parse(this.Id);
    
    public string GenerateFormatedUserName(string name)
    {
        var formatedName = name.Replace(" ", "");
        var formatedId = Guid.NewGuid().ToString("N")[..8];
        return $"{formatedName}_{formatedId}";
    }

    public string GetFormatedUserName()
    {
        return UserName!.Split('@')[0];
    }
}
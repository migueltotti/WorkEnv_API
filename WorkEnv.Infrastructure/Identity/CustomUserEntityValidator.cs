using Microsoft.AspNetCore.Identity;

namespace WorkEnv.Infrastructure.Identity;

public class CustomUserEntityValidator : IUserValidator<ApplicationUser>
{
    public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var emailResult = await manager.FindByEmailAsync(user.Email);

        if (emailResult is not null)
        {
            return IdentityResult.Failed(
                new IdentityError { Description = $"Email {emailResult.Email} is already in use." });
        }
        
        return IdentityResult.Success;
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WorkEnv.Domain.Interfaces;

namespace WorkEnv.Infrastructure.Identity;

public class AppUserManager : UserManager<ApplicationUser>, IAppUserManager
{
    public AppUserManager(
        IUserStore<ApplicationUser> store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<ApplicationUser> passwordHasher, 
        IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        IServiceProvider services, 
        ILogger<UserManager<ApplicationUser>> logger)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
        UserValidators.Clear();
        UserValidators.Add(new CustomUserEntityValidator());
        
        return await base.CreateAsync(user, password);
    } 
}
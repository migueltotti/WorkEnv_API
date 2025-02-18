using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface ITokenManager
{
    string GenerateAccessTokenAsync(User user);
    string GenerateRefreshTokenAsync();
}
using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> VerifyEmail(string email, CancellationToken cancellationToken = default);
    
    Task<bool> SetRefreshToken(Guid userId, string refreshToken, DateTime expirationTime, CancellationToken cancellationToken = default);
    Task<bool> ValidateRefreshToken(Guid userId, string refreshToken, CancellationToken cancellationToken = default);
}
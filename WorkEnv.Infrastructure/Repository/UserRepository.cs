using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Repository;

public class UserRepository(WorkEnvDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Users.FindAsync(userId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await GetAsync(u => u.Email.Equals(email), cancellationToken);
    }

    public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await GetAsync(u => u.Name.Equals(name), cancellationToken);
    }
    
    public async Task<bool> VerifyEmail(string email, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(u => u.Email.Equals(email), cancellationToken);
    }
}
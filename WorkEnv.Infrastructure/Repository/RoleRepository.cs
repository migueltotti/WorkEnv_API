using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Repository;

public class RoleRepository(WorkEnvDbContext context) : Repository<Role>(context), IRoleRepository
{
    public async Task<Role?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleId.Equals(roleId), cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        return await GetAsync(r => roleName.Equals(r.Name), cancellationToken);
    }
}
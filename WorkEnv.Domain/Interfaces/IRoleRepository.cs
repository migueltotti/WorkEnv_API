using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}
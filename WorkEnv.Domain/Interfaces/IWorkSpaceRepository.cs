using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IWorkSpaceRepository : IRepository<WorkSpace>
{
    Task<WorkSpace?> GetByIdAsync(Guid workSpaceId, CancellationToken cancellationToken = default);
}
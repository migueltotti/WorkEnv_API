using WorkEnv.Domain.Entities;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Domain.Interfaces;

public interface ITaskRepository : IRepository<Task>
{
    Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    
    Task<Task?> GetByIdWithUsersAsync(Guid eventId, CancellationToken cancellationToken = default);
}
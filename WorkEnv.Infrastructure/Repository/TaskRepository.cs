using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.Repository;

public class TaskRepository(WorkEnvDbContext context) : Repository<Task>(context), ITaskRepository
{
    public async Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AsNoTracking()
            .Include(t => t.Admin)
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
    }

    public async Task<Task?> GetByIdWithUsersAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AsNoTracking()
            .Include(t => t.Admin)
            .Include(t => t.UserActivities)
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
    }
}
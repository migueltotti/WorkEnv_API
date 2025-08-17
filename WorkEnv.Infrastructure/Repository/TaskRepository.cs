using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.Repository;

public class TaskRepository(WorkEnvDbContext context) : Repository<Task>(context), ITaskRepository
{
    /*public async Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AsNoTracking()
            .Include(t => t.Admin)
            .Include(t => t.WorkSpace)
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
    }

    public async Task<Task?> GetByIdWithUsersAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AsNoTracking()
            .Include(t => t.Admin)
            .Include(t => t.WorkSpace)
            .Include(t => t.UserActivities)
                .ThenInclude(u => u.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
    }*/
    public Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Task?> GetByIdWithUsersAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
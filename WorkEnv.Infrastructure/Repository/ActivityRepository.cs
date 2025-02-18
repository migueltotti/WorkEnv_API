using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.Repository;

public class ActivityRepository(WorkEnvDbContext context) : Repository<Activity>(context), IActicityRepository
{
    public async Task<Activity?> GetByIdAsync(Guid activityId, CancellationToken cancellationToken = default)
    {
        return await context.Set<Event>().Cast<Activity>()
            .Union(context.Set<Task>())
            .Include(e => e.Admin)
            .Include(e => e.WorkSpace)
            .FirstOrDefaultAsync(a => a.Id.Equals(activityId), cancellationToken);
    }

    public new async Task<IEnumerable<Activity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Event>().Cast<Activity>()
            .Union(context.Set<Task>())
            .ToListAsync(cancellationToken);
    }
}
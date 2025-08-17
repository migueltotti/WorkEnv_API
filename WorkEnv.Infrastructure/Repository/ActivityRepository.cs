using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.Repository;

public class ActivityRepository(WorkEnvDbContext context) : Repository<Activity>(context), IActivityRepository
{
    /*
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

    public async Task<List<Activity>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Set<Event>().Cast<Activity>()
            .Union(context.Set<Task>())
            .Where(a => a.Name.Contains(name))
            .ToListAsync(cancellationToken);
    }
    */
    public Task<Activity?> GetByIdAsync(Guid activityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Activity>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
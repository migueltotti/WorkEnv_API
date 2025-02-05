using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.Infrastructure.Repository;

public class EventRepository(WorkEnvDbContext context) : Repository<Event>(context), IEventRepository
{
    public async Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await context.Events
            .AsNoTracking()
            .Include(e => e.Admin)
            .Include(e => e.WorkSpace)
            .FirstOrDefaultAsync(x => x.Id == eventId, cancellationToken);
    }

    public async Task<Event?> GetByIdWithUsersAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await context.Events
            .AsNoTracking()
            .Include(e => e.Admin)
            .Include(e => e.WorkSpace)
            .Include(e => e.UserActivities)
            .FirstOrDefaultAsync(x => x.Id == eventId, cancellationToken);
    }
}
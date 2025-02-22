using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Repository;

public class WorkSpaceRepository(WorkEnvDbContext context) : Repository<WorkSpace>(context), IWorkSpaceRepository
{
    public async Task<WorkSpace?> GetByIdAsync(Guid workSpaceId, CancellationToken cancellationToken = default)
    {
        return await context.WorkSpaces
            .AsNoTracking()
            .Include(ws => ws.Activities)
            .FirstOrDefaultAsync(ws => ws.WorkSpaceId.Equals(workSpaceId), cancellationToken);
    }

    public async Task<List<WorkSpace>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.WorkSpaces
            .AsNoTracking()
            .Where(w => w.OwnerId.Equals(userId))
            .Include(ws => ws.Activities)
            .ToListAsync(cancellationToken);
    }
}
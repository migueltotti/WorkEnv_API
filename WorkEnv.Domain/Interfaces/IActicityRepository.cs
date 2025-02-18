using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IActicityRepository : IRepository<Activity>
{
    Task<Activity?> GetByIdAsync(Guid activityId, CancellationToken cancellationToken = default);
}
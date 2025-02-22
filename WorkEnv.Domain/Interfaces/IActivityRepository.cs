using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IActivityRepository : IRepository<Activity>
{
    Task<Activity?> GetByIdAsync(Guid activityId, CancellationToken cancellationToken = default);
    Task<List<Activity>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<Event?> GetByIdWithUsersAsync(Guid eventId, CancellationToken cancellationToken = default);
}
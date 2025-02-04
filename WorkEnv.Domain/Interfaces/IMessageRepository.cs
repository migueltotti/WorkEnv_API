using WorkEnv.Domain.Entities;

namespace WorkEnv.Domain.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    Task<Message?> GetByIdAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task<Message?> GetByTitleAsync(string roleTitle, CancellationToken cancellationToken = default);
}
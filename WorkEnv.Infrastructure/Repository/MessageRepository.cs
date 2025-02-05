using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.Infrastructure.Repository;

public class MessageRepository(WorkEnvDbContext context) : Repository<Message>(context), IMessageRepository
{
    public async Task<Message?> GetByIdAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await context.Messages
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MessageId.Equals(messageId), cancellationToken);
    }

    public async Task<Message?> GetByTitleAsync(string messageTitle, CancellationToken cancellationToken = default)
    {
        return await GetAsync(m => m.Title.Equals(messageTitle), cancellationToken);
    }
}
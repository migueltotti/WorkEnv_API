using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheMessageRepository : IMessageRepository
{
    private readonly MessageRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheMessageRepository(MessageRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<Message?> GetByIdAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var key = messageId.ToString();
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        Message? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByIdAsync(messageId, cancellationToken);

            if (entity is null) return entity;
            
            await _cache.SetStringAsync(
                key, 
                JsonSerializer.Serialize(entity), 
                cancellationToken);
            
            return entity;
        }
        
        entity = JsonConvert.DeserializeObject<Message>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }

    
    public Task<Message?> GetAsync(Expression<Func<Message, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task<IEnumerable<Message>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public Task AddAsync(Message entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(Message entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(Message entity)
    {
        _decorator.Delete(entity);
    }
    
    public Task<Message?> GetByTitleAsync(string messageTitle, CancellationToken cancellationToken = default)
    {
        return _decorator.GetByTitleAsync(messageTitle, cancellationToken);
    }
}
using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheEventRepository : IEventRepository
{
    private readonly EventRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheEventRepository(EventRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        var key = eventId.ToString();
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        Event? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByIdAsync(eventId, cancellationToken);

            if (entity is null) return entity;
            
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            
            await _cache.SetStringAsync(
                key, 
                JsonSerializer.Serialize(entity),
                cacheOptions,
                cancellationToken);
            
            return entity;
        }
        
        entity = JsonConvert.DeserializeObject<Event>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }
    
    public Task<Event?> GetAsync(Expression<Func<Event, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public Task AddAsync(Event entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(Event entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(Event entity)
    {
        _decorator.Delete(entity);
    }

    public Task<Event?> GetByIdWithUsersAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return _decorator.GetByIdWithUsersAsync(eventId, cancellationToken);
    }
}
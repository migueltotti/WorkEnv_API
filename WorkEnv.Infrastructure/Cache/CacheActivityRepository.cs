using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheActivityRepository : IActivityRepository
{
    private readonly ActivityRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheActivityRepository(ActivityRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<Activity?> GetByIdAsync(Guid activityId, CancellationToken cancellationToken = default)
    {
        var key = activityId.ToString();
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        Activity? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByIdAsync(activityId, cancellationToken);

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
        
        entity = JsonConvert.DeserializeObject<Activity>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }
    
    public Task<Activity?> GetAsync(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task AddAsync(Activity entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(Activity entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(Activity entity)
    {
        _decorator.Delete(entity);
    }

    public Task<IEnumerable<Activity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public Task<List<Activity>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return _decorator.GetByNameAsync(name, cancellationToken);
    }
}
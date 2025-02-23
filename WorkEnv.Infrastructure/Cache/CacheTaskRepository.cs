using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheTaskRepository : ITaskRepository
{
    private readonly TaskRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheTaskRepository(TaskRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<Task?> GetByIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        var key = taskId.ToString();
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        Task? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByIdAsync(taskId, cancellationToken);

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
        
        entity = JsonConvert.DeserializeObject<Task>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }
    
    public Task<Task?> GetAsync(Expression<Func<Task, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task<IEnumerable<Task>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public System.Threading.Tasks.Task AddAsync(Task entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(Task entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(Task entity)
    {
        _decorator.Delete(entity);
    }

    public Task<Task?> GetByIdWithUsersAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return _decorator.GetByIdWithUsersAsync(taskId, cancellationToken);
    }
}
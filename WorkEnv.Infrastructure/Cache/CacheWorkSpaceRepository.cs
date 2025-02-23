using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheWorkSpaceRepository : IWorkSpaceRepository
{
    private readonly WorkSpaceRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheWorkSpaceRepository(WorkSpaceRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<WorkSpace?> GetByIdAsync(Guid workSpaceId, CancellationToken cancellationToken = default)
    {
        var key = workSpaceId.ToString();
        
        var workSpace = await _cache.GetStringAsync(key, cancellationToken);

        WorkSpace? entity;

        if (String.IsNullOrEmpty(workSpace))
        {
            entity = await _decorator.GetByIdAsync(workSpaceId, cancellationToken);

            if (entity is null) return entity;

            await _cache.SetStringAsync(
                key,
                JsonSerializer.Serialize(entity),
                cancellationToken);
            
            return entity;
        }
        
        entity = JsonConvert.DeserializeObject<WorkSpace>(
            workSpace,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }
    
    public Task<WorkSpace?> GetAsync(Expression<Func<WorkSpace, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task<IEnumerable<WorkSpace>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public Task AddAsync(WorkSpace entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(WorkSpace entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(WorkSpace entity)
    {
        _decorator.Delete(entity);
    }

    public Task<List<WorkSpace>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllByUserIdAsync(userId, cancellationToken);
    }
}
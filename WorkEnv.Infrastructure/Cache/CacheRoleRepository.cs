using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheRoleRepository : IRoleRepository
{
    private readonly RoleRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheRoleRepository(RoleRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<Role?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var key = roleId.ToString();
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        Role? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByIdAsync(roleId, cancellationToken);

            if (entity is null) return entity;
            
            await _cache.SetStringAsync(
                key, 
                JsonSerializer.Serialize(entity), 
                cancellationToken);
            
            return entity;
        }
        
        entity = JsonConvert.DeserializeObject<Role>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }
    
    public Task<Role?> GetAsync(Expression<Func<Role, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public Task AddAsync(Role entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(Role entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(Role entity)
    {
        _decorator.Delete(entity);
    }

    public Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        return _decorator.GetByNameAsync(roleName, cancellationToken);
    }
}
using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Repository;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = System.Threading.Tasks.Task;

namespace WorkEnv.Infrastructure.Cache;

public class CacheUserRepository : IUserRepository
{
    private readonly UserRepository _decorator;
    private readonly IDistributedCache _cache;

    public CacheUserRepository(UserRepository decorator, IDistributedCache cache)
    {
        _decorator = decorator;
        _cache = cache;
    }
    
    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var key = userId.ToString();
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        User? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByIdAsync(userId, cancellationToken);

            if (entity is null) return entity;
            
            await _cache.SetStringAsync(
                key, 
                JsonSerializer.Serialize(entity), 
                cancellationToken);
            
            return entity;
        }
        
        entity = JsonConvert.DeserializeObject<User>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var key = email;
        
        var user = await _cache.GetStringAsync(key, cancellationToken);

        User? entity;

        if (string.IsNullOrEmpty(user))
        {
            entity = await _decorator.GetByEmailAsync(email, cancellationToken);

            if (entity is null) return entity;
            
            await _cache.SetStringAsync(
                key, 
                JsonSerializer.Serialize(entity), 
                cancellationToken);
            
            return entity;
        }
        
        entity = JsonConvert.DeserializeObject<User>(
            user,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });
        
        return entity;
    }
    
    public Task<User?> GetAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _decorator.GetAsync(expression, cancellationToken);
    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _decorator.GetAllAsync(cancellationToken);
    }

    public Task AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        return _decorator.AddAsync(entity, cancellationToken);
    }

    public void Update(User entity)
    {
        _decorator.Update(entity);
    }

    public void Delete(User entity)
    {
        _decorator.Delete(entity);
    }

    public Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return _decorator.GetByNameAsync(name, cancellationToken);
    }

    public Task<bool> VerifyEmail(string email, CancellationToken cancellationToken = default)
    {
        return _decorator.VerifyEmail(email, cancellationToken);
    }

    public Task<bool> SetRefreshToken(Guid userId, string refreshToken, DateTime expirationTime,
        CancellationToken cancellationToken = default)
    {
        return _decorator.SetRefreshToken(userId, refreshToken, expirationTime, cancellationToken);
    }

    public Task<bool> ValidateRefreshToken(Guid userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        return _decorator.ValidateRefreshToken(userId, refreshToken, cancellationToken);
    }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Interfaces;
using WorkEnv.Infrastructure.Context;

namespace WorkEnv.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private WorkEnvDbContext _context;

    public Repository(WorkEnvDbContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        _context.Set<T>().Remove(entity);
    }
}
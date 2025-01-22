using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorkEnv.Infrastructure.Context;

public class WorkEnvDbContext : DbContext
{
    public WorkEnvDbContext(DbContextOptions<WorkEnvDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkEnvDbContext).Assembly);
    }
}
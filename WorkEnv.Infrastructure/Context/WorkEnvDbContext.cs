using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.ValueObjects;
using Activity = System.Diagnostics.Activity;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.Context;

public class WorkEnvDbContext : DbContext
{
    public WorkEnvDbContext(DbContextOptions<WorkEnvDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<WorkSpace> WorkSpaces { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkEnvDbContext).Assembly);
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkEnv.Infrastructure.Identity;

namespace WorkEnv.Infrastructure.Context;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.HasIndex(r => r.UserName).IsUnique(false);
            b.HasIndex(r => r.NormalizedUserName).IsUnique(false);
            b.HasIndex(r => r.Email).IsUnique();
            b.HasIndex(r => r.NormalizedEmail).IsUnique();
            
            b.Property(r => r.UserName).HasMaxLength(80);
            b.Property(r => r.NormalizedUserName).HasMaxLength(80);
            
            b.Property(r => r.Email).HasMaxLength(100);
            b.Property(r => r.NormalizedEmail).HasMaxLength(100);
        });
    }
}
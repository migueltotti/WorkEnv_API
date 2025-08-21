using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(r => r.Permissions)
            .HasConversion<Permission>()
            .IsRequired();

        builder.HasMany(r => r.EventParticipants)
            .WithOne(ep => ep.Role)
            .HasForeignKey(ep => ep.RoleId)
            .IsRequired(false);
        builder.HasMany(r => r.Collaborators)
            .WithOne(c => c.Role)
            .HasForeignKey(c => c.RoleId)
            .IsRequired(false);
    }
}
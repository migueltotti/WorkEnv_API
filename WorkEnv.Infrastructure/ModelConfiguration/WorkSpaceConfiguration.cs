using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class WorkSpaceConfiguration : IEntityTypeConfiguration<WorkSpace>
{
    public void Configure(EntityTypeBuilder<WorkSpace> builder)
    {
        builder.ToTable("WorkSpace");

        builder.HasKey(ws => ws.Id);
        
        builder.Property(ws => ws.Id)
            .IsRequired();
        builder.Property(ws => ws.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(ws => ws.OwnerId)
            .IsRequired();
        builder.Property(ws => ws.RegisteredAt)
            .IsRequired();
        builder.Property(ws => ws.NumberOfActivities)
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(ws => ws.NumberOfCollaborators)
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(ws => ws.SecretKey)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.HasMany(ws => ws.Activities)
            .WithOne(a => a.WorkSpace)
            .HasForeignKey(a => a.WorkSpaceId);
        builder.HasMany(ws => ws.Messages)
            .WithOne(m => m.WorkSpace)
            .HasForeignKey(m => m.WorkSpaceId);
        builder.HasMany(ws => ws.Roles)
            .WithOne(r => r.WorkSpace)
            .HasForeignKey(r => r.WorkSpaceId);
        builder.HasMany(ws => ws.Collaborators)
            .WithOne(c => c.WorkSpace)
            .HasForeignKey(c => c.WorkSpaceId);
            
    }
}
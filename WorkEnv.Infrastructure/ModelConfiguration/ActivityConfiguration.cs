using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activity");

        builder.HasKey(a => a.Id);
        
        builder.HasDiscriminator<string>("ActivityType")
            .HasValue<Event>("Event")
            .HasValue<Task>("Task");
        
        builder.Property(a => a.WorkSpaceId)
            .IsRequired();
        builder.Property(a => a.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(a => a.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(a => a.CreatedAt)
            .IsRequired();
        builder.Property(a => a.StartDate)
            .IsRequired();
        builder.Property(a => a.EndDate)
            .IsRequired();

        builder.HasMany(a => a.Messages)
            .WithOne(m => m.Activity)
            .HasForeignKey(m => m.ActivityId)
            .IsRequired(false);
    }
}
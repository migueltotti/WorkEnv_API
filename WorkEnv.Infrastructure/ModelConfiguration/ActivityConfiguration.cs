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
        builder.Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(a => a.NumberOfParticipants)
            .HasDefaultValue(1);
        builder.Property(a => a.MaxNumberOfParticipants)
            .IsRequired();
        builder.Property(a => a.Privacy)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(a => a.ActivityStatus)
            .IsRequired()
            .HasDefaultValue(TaskStatus.Created)
            .HasConversion<string>();
        builder.Property(a => a.AccessPassword)
            .HasMaxLength(12);
        builder.Property(a => a.AccessOptions)
            .IsRequired()
            .HasConversion<string>();
        
        builder.OwnsOne(a => a.AdminInviteCode);

        builder.HasOne(a => a.Admin)
            .WithMany()
            .HasForeignKey(a => a.AdminId)
            .IsRequired(false);
        builder.HasMany(a => a.Messages)
            .WithOne(m => m.Activity)
            .HasForeignKey(m => m.ActivityId);
        builder.HasMany(a => a.UserActivities)
            .WithOne(u => u.Activity)
            .HasForeignKey(u => u.ActivityId);
    }
}
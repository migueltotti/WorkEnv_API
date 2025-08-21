using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasDefaultValue(TaskStatus.Created)
            .IsRequired();
        builder.Property(t => t.TaskPriority)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(t => t.NumberOfAssignedUsers)
            .HasDefaultValue(0);
        
        builder.HasMany(t => t.AssignedUsers)
            .WithOne(ta => ta.Task)
            .HasForeignKey(ta => ta.TaskId);
    }
}
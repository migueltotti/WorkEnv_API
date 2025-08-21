using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> builder)
    {
        builder.HasKey(ta => new { ta.TaskId, ta.AssignedUserId });
        
        builder.Property(ta => ta.AssignedAt)
            .IsRequired();
    }
}
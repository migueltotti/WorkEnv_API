using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
{
    public void Configure(EntityTypeBuilder<UserActivity> builder)
    {
        builder.HasKey(ura => new { ura.UserId, ura.ActivityId });
        
        builder.Property(ura => ura.UserId)
            .IsRequired();
        builder.Property(ura => ura.ActivityId)
            .IsRequired();
    }
}
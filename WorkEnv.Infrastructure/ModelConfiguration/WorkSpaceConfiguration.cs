using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class WorkSpaceConfiguration : IEntityTypeConfiguration<WorkSpace>
{
    public void Configure(EntityTypeBuilder<WorkSpace> builder)
    {
        builder.ToTable("WorkSpace");

        builder.HasKey(ws => ws.WorkSpaceId);
        
        builder.Property(ws => ws.WorkSpaceId)
            .IsRequired();
        builder.Property(ws => ws.OwnerId)
            .IsRequired();
        builder.Property(ws => ws.NumberOfActivities)
            .IsRequired()
            .HasDefaultValue(0);
        builder.Property<string>("_masterCode")
            .IsRequired()
            .HasColumnName("_masterCode")
            .HasMaxLength(30);

        builder.HasMany(ws => ws.Activities)
            .WithOne(a => a.WorkSpace)
            .HasForeignKey(a => a.WorkSpaceId);
    }
}
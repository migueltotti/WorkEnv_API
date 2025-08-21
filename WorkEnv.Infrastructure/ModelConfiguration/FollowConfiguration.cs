using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.HasKey(f => new { f.FollowerId, f.FolloweeId });

        builder.Property(f => f.Status)
            .HasDefaultValue(FollowStatus.Pending)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(f => f.RequestedAt)
            .IsRequired();
    }
}
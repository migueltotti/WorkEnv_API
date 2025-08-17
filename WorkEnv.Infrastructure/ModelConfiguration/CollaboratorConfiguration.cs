using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class CollaboratorConfiguration : IEntityTypeConfiguration<Collaborator>
{
    public void Configure(EntityTypeBuilder<Collaborator> builder)
    {
        builder.HasKey(c => new { c.WorkSpaceId, c.UserId });
        
        builder.Property(c => c.JoinedAt)
            .IsRequired();
        
    }
}
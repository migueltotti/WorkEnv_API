using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        builder.ToTable("EventParticipant");
        
        builder.HasKey(ep => new {ep.UserId, ep.EventId});

        builder.Property(ep => ep.RegisteredAt)
            .IsRequired();
    }
}
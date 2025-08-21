using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Address)
            .IsRequired();
        builder.Property(e => e.NumberOfParticipants)
            .HasDefaultValue(0)
            .IsRequired();
        builder.Property(e => e.MaxNumberOfParticipants)
            .IsRequired();
        builder.Property(e => e.Privacy)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(e => e.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(e => e.AccessOption)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(e => e.AccessKey)
            .IsRequired();
        builder.Property(e => e.AdminAdmittedAt)
            .IsRequired();

        builder.OwnsOne(e => e.AdminInvite, ai =>
        {
            ai.Property(p => p.Code)
                .HasMaxLength(6);
        });

        builder.HasMany(e => e.Participants)
            .WithOne(e => e.Event)
            .HasForeignKey(e => e.EventId);
        builder.HasOne(e => e.Admin)
            .WithMany(e => e.AdminEvent)
            .HasForeignKey(e => e.AdminId);
    }
}
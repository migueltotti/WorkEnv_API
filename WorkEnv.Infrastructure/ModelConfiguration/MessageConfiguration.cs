using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Message");

        builder.HasKey(m => m.MessageId);
        
        builder.Property(m => m.ActivityId)
            .IsRequired();
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(m => m.PublishedAt)
            .IsRequired();
    }
}
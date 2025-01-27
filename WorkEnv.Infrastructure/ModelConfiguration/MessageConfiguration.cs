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
            .HasMaxLength(80);
        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(m => m.CreateDate)
            .IsRequired();
        builder.Property(m => m.MessageType)
            .IsRequired()
            .HasConversion<string>(); //   ISADORA
    }
}
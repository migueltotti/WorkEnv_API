using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired();
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(u => u.CpfCnpj)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(u => u.DateBirth)
            .IsRequired();
        builder.Property(u => u.ProfilePicture)
            .HasMaxLength(500);
        builder.Property(u => u.PersonalDescription)
            .HasMaxLength(1500);
        builder.Property(u => u.RegisteredAt)
            .IsRequired();
        builder.Property(u => u.Privacy)
            .IsRequired()
            .HasConversion<string>();

        
        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.HasIndex(u => u.CpfCnpj)
            .IsUnique();

        builder.HasMany(u => u.WorkSpaces)
            .WithOne(ws => ws.Owner)
            .HasForeignKey(ws => ws.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Collaborations)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
        
        builder.HasMany(u => u.Followers)
            .WithOne(fwers => fwers.Followee)
            .HasForeignKey(fwers => fwers.FolloweeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(u => u.Following)
            .WithOne(fwign => fwign.Follower)
            .HasForeignKey(fwign => fwign.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.EventsParticipant)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
    }
}
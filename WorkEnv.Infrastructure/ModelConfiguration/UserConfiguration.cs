using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkEnv.Domain.Entities;

namespace WorkEnv.Infrastructure.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .IsRequired();
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(80);
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(30);
        builder.Property<string>("_refreshToken")
            .HasColumnName("_refreshToken")
            .HasMaxLength(200);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasMany(u => u.WorkSpaces)
            .WithOne(ws => ws.Owner)
            .HasForeignKey(ws => ws.OwnerId);
        builder.HasMany(u => u.UserActivities)
            .WithOne(ura => ura.User)
            .HasForeignKey(ura => ura.UserId);
    }
}
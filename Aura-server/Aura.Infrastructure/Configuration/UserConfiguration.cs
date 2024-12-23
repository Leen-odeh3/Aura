using Aura.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Aura.Infrastructure.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder.HasMany(u => u.Stories)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder.Property(u => u.ImageId).IsRequired(false);

    }
}
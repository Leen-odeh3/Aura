using Aura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aura.Infrastructure.Configuration;
public class PrivateMessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
              builder.HasOne(s => s.Sender)
                     .WithMany(g => g.SendedPrivateMessages)
                     .HasForeignKey(s => s.SenderId)
                     .OnDelete(DeleteBehavior.Restrict);

         builder.HasOne(s => s.Receiver)
                .WithMany(g => g.ReceivedPrivateMessages)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

    }
}

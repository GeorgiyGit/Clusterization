using Domain.Entities.DataSources.Telegram;
using Domain.Entities.DataSources.Youtube;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.DataSources.Telegram
{
    internal class TelegramMessageBuilder : IEntityTypeConfiguration<TelegramMessage>
    {
        public void Configure(EntityTypeBuilder<TelegramMessage> builder)
        {
            builder.HasMany(e => e.TelegramReplies)
                   .WithOne(e => e.TelegramMessage)
                   .HasForeignKey(e => e.TelegramMessageId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Reactions)
                   .WithOne(e => e.TelegramMessage)
                   .HasForeignKey(e => e.TelegramMessageId)
                   .IsRequired(false);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.LoadedTelegramMessages)
                   .HasForeignKey(e=>e.LoaderId);

            builder.HasOne(e => e.TelegramChannel)
                   .WithMany(e => e.TelegramMessages)
                   .HasForeignKey(e => e.TelegramChannelId);
        }
    }
}

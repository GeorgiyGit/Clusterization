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
    internal class TelegramChannelBuilder : IEntityTypeConfiguration<TelegramChannel>
    {
        public void Configure(EntityTypeBuilder<TelegramChannel> builder)
        {
            builder.HasMany(e => e.TelegramMessages)
                   .WithOne(e => e.TelegramChannel)
                   .HasForeignKey(e => e.TelegramChannelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.TelegramReplies)
                   .WithOne(e => e.TelegramChannel)
                   .HasForeignKey(e => e.TelegramChannelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.LoadedTelegramChannels)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}

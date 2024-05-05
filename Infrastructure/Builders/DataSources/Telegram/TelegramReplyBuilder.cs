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
    internal class TelegramReplyBuilder : IEntityTypeConfiguration<TelegramReply>
    {
        public void Configure(EntityTypeBuilder<TelegramReply> builder)
        {
            builder.HasOne(e => e.TelegramChannel)
                   .WithMany(e => e.TelegramReplies)
                   .HasForeignKey(e => e.TelegramChannelId);

            builder.HasOne(e => e.TelegramMessage)
                   .WithMany(e => e.TelegramReplies)
                   .HasForeignKey(e => e.TelegramMessageId);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.LoadedTelegramReplies)
                   .HasForeignKey(e => e.LoaderId);

            builder.HasMany(e => e.Reactions)
                   .WithOne(e => e.TelegramReply)
                   .HasForeignKey(e => e.TelegramReplyId)
                   .IsRequired(false);

            builder.HasOne(e => e.DataObject)
                   .WithOne(e => e.TelegramReply)
                   .HasForeignKey<TelegramReply>(e => e.DataObjectId)
                   .IsRequired(false);

            builder.HasIndex(e => new { e.Date });
        }
    }
}

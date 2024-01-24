using Domain.Entities.Youtube;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Youtube
{
    internal class ChannelBuilder
    {
        public static void ChannelBuild(EntityTypeBuilder<Channel> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Comments)
                        .WithOne(e => e.Channel)
                        .HasForeignKey(e => e.ChannelId);

            modelBuilder.HasIndex(e => new { e.PublishedAtDateTimeOffset, e.VideoCount, e.SubscriberCount });

            modelBuilder.HasMany(e => e.Videos)
                        .WithOne(e => e.Channel)
                        .HasForeignKey(e => e.ChannelId);
        }
    }
}

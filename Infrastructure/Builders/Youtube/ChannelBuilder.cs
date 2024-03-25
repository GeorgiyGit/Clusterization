using Domain.Entities.Youtube;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Youtube
{
    internal class ChannelBuilder : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.HasMany(e => e.Comments)
                   .WithOne(e => e.Channel)
                   .HasForeignKey(e => e.ChannelId);

            builder.HasIndex(e => new { e.PublishedAtDateTimeOffset, e.VideoCount, e.SubscriberCount });

            builder.HasMany(e => e.Videos)
                   .WithOne(e => e.Channel)
                   .HasForeignKey(e => e.ChannelId);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.Channels)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}

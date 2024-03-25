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
    internal class VideoBuilder : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasMany(e => e.Comments)
                   .WithOne(e => e.Video)
                   .HasForeignKey(e => e.VideoId);

            builder.HasIndex(e => new { e.PublishedAtDateTimeOffset, e.CommentCount, e.ViewCount });

            builder.HasOne(e => e.Channel)
                   .WithMany(e => e.Videos)
                   .HasForeignKey(e => e.ChannelId);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.Videos)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}

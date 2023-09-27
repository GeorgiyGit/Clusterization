using Domain.Entities.Youtube;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Youtube
{
    internal class VideoBuilder
    {
        public static void VideoBuild(EntityTypeBuilder<Video> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Comments)
                        .WithOne(e => e.Video)
                        .HasForeignKey(e => e.VideoId);

            modelBuilder.HasOne(e => e.Channel)
                        .WithMany(e => e.Videos)
                        .HasForeignKey(e => e.ChannelId);
        }
    }
}

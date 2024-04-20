﻿using Domain.Entities.DataSources.Youtube;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DataSources.Youtube
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
                   .HasForeignKey(e => e.ChannelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.Videos)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}
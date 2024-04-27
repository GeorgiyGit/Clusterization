using Domain.Entities.DataSources.Youtube;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DataSources.Youtube
{
    internal class YoutubeChannelBuilder : IEntityTypeConfiguration<YoutubeChannel>
    {
        public void Configure(EntityTypeBuilder<YoutubeChannel> builder)
        {
            builder.HasMany(e => e.Comments)
                   .WithOne(e => e.Channel)
                   .HasForeignKey(e => e.ChannelId);

            builder.HasIndex(e => new { e.PublishedAtDateTimeOffset, e.VideoCount, e.SubscriberCount });

            builder.HasMany(e => e.Videos)
                   .WithOne(e => e.Channel)
                   .HasForeignKey(e => e.ChannelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.Channels)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}

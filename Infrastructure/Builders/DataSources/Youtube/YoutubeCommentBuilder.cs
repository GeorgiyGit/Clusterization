using Domain.Entities.DataSources.Youtube;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DataSources.Youtube
{
    internal class YoutubeCommentBuilder : IEntityTypeConfiguration<YoutubeComment>
    {
        public void Configure(EntityTypeBuilder<YoutubeComment> builder)
        {
            builder.HasOne(e => e.Video)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(e => e.VideoId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(e => new { e.PublishedAtDateTimeOffset });

            builder.HasOne(e => e.Channel)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(e => e.ChannelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.DataObject)
                   .WithOne(e => e.Comment)
                   .HasForeignKey<YoutubeComment>(e => e.DataObjectId)
                   .IsRequired(false);


            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.LoadedYoutubeComments)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}

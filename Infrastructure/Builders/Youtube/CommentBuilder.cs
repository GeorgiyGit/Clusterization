using Domain.Entities.Embeddings;
using Domain.Entities.Tasks;
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
    internal class CommentBuilder : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(e => e.Video)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(e => e.VideoId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.HasIndex(e => new { e.PublishedAtDateTimeOffset });

            builder.HasOne(e => e.Channel)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(e => e.ChannelId);

            builder.HasOne(e => e.EmbeddingData)
                   .WithOne(e => e.Comment)
                   .HasForeignKey<EmbeddingData>(e => e.CommentId)
                   .IsRequired(false);

            builder.HasMany(e => e.Workspaces)
                   .WithMany(e => e.Comments);

            builder.HasMany(e => e.ClusterizationEntities)
                   .WithOne(e => e.Comment)
                   .HasForeignKey(e => e.CommentId);


            builder.HasOne(e => e.Loader)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(e => e.LoaderId);
        }
    }
}

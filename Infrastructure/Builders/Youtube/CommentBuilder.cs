using Domain.Entities.Embeddings;
using Domain.Entities.Youtube;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Youtube
{
    internal class CommentBuilder
    {
        public static void CommentBuild(EntityTypeBuilder<Comment> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Video)
                        .WithMany(e => e.Comments)
                        .HasForeignKey(e => e.VideoId)
                        .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            modelBuilder.HasIndex(e => new { e.PublishedAtDateTimeOffset });

            modelBuilder.HasOne(e => e.Channel)
                        .WithMany(e => e.Comments)
                        .HasForeignKey(e => e.ChannelId);

            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithOne(e => e.Comment)
                        .HasForeignKey<EmbeddingData>(e => e.CommentId)
                        .IsRequired(false);

            modelBuilder.HasMany(e => e.Workspaces)
                        .WithMany(e => e.Comments);

            modelBuilder.HasMany(e => e.ClusterizationEntities)
                        .WithOne(e => e.Comment)
                        .HasForeignKey(e => e.CommentId);


            modelBuilder.HasOne(e => e.Loader)
                        .WithMany(e => e.Comments)
                        .HasForeignKey(e => e.LoaderId);
        }
    }
}

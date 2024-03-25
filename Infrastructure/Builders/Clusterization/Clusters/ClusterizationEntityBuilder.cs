using Domain.Entities.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Clusters
{
    internal class ClusterizationEntityBuilder : IEntityTypeConfiguration<ClusterizationEntity>
    {
        public void Configure(EntityTypeBuilder<ClusterizationEntity> builder)
        {
            builder.HasOne(e => e.EmbeddingData)
                   .WithMany(e => e.Entities)
                   .HasForeignKey(e => e.EmbeddingDataId)
                   .IsRequired(false);

            builder.HasOne(e => e.Comment)
                   .WithMany(e => e.ClusterizationEntities)
                   .HasForeignKey(e => e.CommentId);

            builder.HasMany(e => e.Clusters)
                   .WithMany(e => e.Entities);

            builder.HasOne(e => e.Workspace)
                   .WithMany(e => e.Entities)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.DimensionalityReductionValues)
                   .WithOne(e => e.ClusterizationEntity)
                   .HasForeignKey(e => e.ClusterizationEntityId);

            builder.HasOne(e => e.ExternalObject)
                   .WithMany(e => e.ClusterizationEntities)
                   .HasForeignKey(e => e.ExternalObjectId)
                   .IsRequired(false);
        }
    }
}

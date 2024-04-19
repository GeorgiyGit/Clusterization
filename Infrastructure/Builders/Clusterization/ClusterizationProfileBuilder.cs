using Domain.Entities.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization
{
    internal class ClusterizationProfileBuilder : IEntityTypeConfiguration<ClusterizationProfile>
    {
        public void Configure(EntityTypeBuilder<ClusterizationProfile> builder)
        {
            builder.HasOne(e => e.Algorithm)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.AlgorithmId);

            builder.HasOne(e => e.DimensionType)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.DimensionCount);

            builder.HasOne(e => e.Workspace)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.WorkspaceId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Clusters)
                   .WithOne(e => e.Profile)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasMany(e => e.Tiles)
                   .WithOne(e => e.Profile)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasMany(e => e.TilesLevels)
                   .WithOne(e => e.Profile)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasOne(e => e.DRTechnique)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.DRTechniqueId);

            builder.HasOne(e => e.Owner)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.OwnerId);

            builder.HasOne(e => e.EmbeddingModel)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.EmbeddingModelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.EmbeddingLoadingState)
                   .WithOne(e => e.Profile)
                   .HasForeignKey<ClusterizationProfile>(e => e.EmbeddingLoadingStateId);
        }
    }
}

using Domain.Entities.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Profiles
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
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.Clusters)
                   .WithOne(e => e.Profile)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasMany(e => e.Tiles)
                   .WithOne(e => e.Profile)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasMany(e => e.TilesLevels)
                   .WithOne(e => e.Profile)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasOne(e => e.DimensionalityReductionTechnique)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.DimensionalityReductionTechniqueId);

            builder.HasOne(e => e.Owner)
                   .WithMany(e => e.Profiles)
                   .HasForeignKey(e => e.OwnerId);
        }
    }
}

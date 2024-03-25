using Domain.Entities.DimensionalityReduction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.DimensionalityReduction
{
    internal class DimensionalityReductionValueBuilder : IEntityTypeConfiguration<DimensionalityReductionValue>
    {
        public void Configure(EntityTypeBuilder<DimensionalityReductionValue> builder)
        {
            builder.HasMany(e => e.Embeddings)
                   .WithOne(e => e.DimensionalityReductionValue)
                   .HasForeignKey(e => e.DimensionalityReductionValueId);

            builder.HasIndex(e => new { e.TechniqueId, e.ClusterizationWorkspaceDRTechniqueId });

            builder.HasOne(e => e.EmbeddingData)
                   .WithOne(e => e.DimensionalityReductionValue)
                   .HasForeignKey<DimensionalityReductionValue>(e => e.EmbeddingDataId);

            builder.HasOne(e => e.Technique)
                   .WithMany(e => e.Values)
                   .HasForeignKey(e => e.TechniqueId);

            builder.HasOne(e => e.ClusterizationEntity)
                   .WithMany(e => e.DimensionalityReductionValues)
                   .HasForeignKey(e => e.ClusterizationEntityId);

            builder.HasOne(e => e.ClusterizationWorkspaceDRTechnique)
                   .WithMany(e => e.DRValues)
                   .HasForeignKey(e => e.ClusterizationWorkspaceDRTechniqueId)
                   .IsRequired(false);
        }
    }
}

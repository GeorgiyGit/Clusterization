using Domain.Entities.Embeddings;
using Domain.Entities.Tasks;
using Domain.Entities.Youtube;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders
{
    internal class EmbeddingsBuilder
    {
        public static void BuildAll(ModelBuilder modelBuilder)
        {
            EmbeddingDataBuild(modelBuilder.Entity<EmbeddingData>());
            DimensionValueBuild(modelBuilder.Entity<EmbeddingDimensionValue>());
        }
        public static void EmbeddingDataBuild(EntityTypeBuilder<EmbeddingData> modelBuilder)
        {
            modelBuilder.HasOne(e => e.DimensionalityReductionValue)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey<EmbeddingData>(e => e.DimensionalityReductionValueId);

            modelBuilder.HasOne(e => e.Comment)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey<Comment>(e => e.EmbeddingDataId);

            modelBuilder.HasMany(e => e.Entities)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey(e => e.EmbeddingDataId);

            modelBuilder.HasOne(e => e.OriginalEmbedding)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey<EmbeddingData>(e => e.OriginalEmbeddingId);
        }
        public static void DimensionValueBuild(EntityTypeBuilder<EmbeddingDimensionValue> modelBuilder)
        {
            modelBuilder.HasOne(e => e.DimensionType)
                        .WithMany(e => e.DimensionValues)
                        .HasForeignKey(e => e.DimensionTypeId);

            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithOne(e => e.OriginalEmbedding)
                        .HasForeignKey<EmbeddingDimensionValue>(e => e.EmbeddingDataId)
                        .IsRequired(false);

            modelBuilder.HasOne(e => e.DimensionalityReductionValue)
                        .WithMany(e => e.Embeddings)
                        .HasForeignKey(e => e.DimensionalityReductionValueId)
                        .IsRequired(false);
        }
    }
}

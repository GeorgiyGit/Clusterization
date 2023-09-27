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
            EmdeddingValueBuild(modelBuilder.Entity<EmbeddingValue>());
        }
        public static void EmbeddingDataBuild(EntityTypeBuilder<EmbeddingData> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Embeddings)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey(e => e.EmbeddingDataId);

            modelBuilder.HasOne(e => e.Comment)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey<Comment>(e => e.EmbeddingDataId);

            modelBuilder.HasMany(e => e.Entities)
                        .WithOne(e => e.EmbeddingData)
                        .HasForeignKey(e => e.EmbeddingDataId);
        }
        public static void DimensionValueBuild(EntityTypeBuilder<EmbeddingDimensionValue> modelBuilder)
        {
            modelBuilder.HasOne(e => e.DimensionType)
                        .WithMany(e => e.DimensionValues)
                        .HasForeignKey(e => e.DimensionTypeId);

            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithMany(e => e.Embeddings)
                        .HasForeignKey(e => e.EmbeddingDataId);

            modelBuilder.HasMany(e => e.Values)
                        .WithOne(e => e.EmbeddingDimensionValue)
                        .HasForeignKey(e => e.EmbeddingDimensionValueId);
        }
        public static void EmdeddingValueBuild(EntityTypeBuilder<EmbeddingValue> modelBuilder)
        {
            modelBuilder.HasOne(e => e.EmbeddingDimensionValue)
                        .WithMany(e => e.Values)
                        .HasForeignKey(e => e.EmbeddingDimensionValueId);
        }
    }
}

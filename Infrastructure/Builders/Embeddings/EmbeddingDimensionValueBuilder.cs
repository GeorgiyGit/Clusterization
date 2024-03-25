using Domain.Entities.Embeddings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Embeddings
{
    internal class EmbeddingDimensionValueBuilder : IEntityTypeConfiguration<EmbeddingDimensionValue>
    {
        public void Configure(EntityTypeBuilder<EmbeddingDimensionValue> builder)
        {
            builder.HasOne(e => e.DimensionType)
                   .WithMany(e => e.DimensionValues)
                   .HasForeignKey(e => e.DimensionTypeId);

            builder.HasOne(e => e.EmbeddingData)
                   .WithOne(e => e.OriginalEmbedding)
                   .HasForeignKey<EmbeddingDimensionValue>(e => e.EmbeddingDataId)
                   .IsRequired(false);

            builder.HasOne(e => e.DimensionalityReductionValue)
                   .WithMany(e => e.Embeddings)
                   .HasForeignKey(e => e.DimensionalityReductionValueId)
                   .IsRequired(false);
        }
    }
}

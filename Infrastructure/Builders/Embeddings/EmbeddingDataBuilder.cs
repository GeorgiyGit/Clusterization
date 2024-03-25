using Domain.Entities.Embeddings;
using Domain.Entities.Youtube;
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
    internal class EmbeddingDataBuilder : IEntityTypeConfiguration<EmbeddingData>
    {
        public void Configure(EntityTypeBuilder<EmbeddingData> builder)
        {
            builder.HasOne(e => e.DimensionalityReductionValue)
                   .WithOne(e => e.EmbeddingData)
                   .HasForeignKey<EmbeddingData>(e => e.DimensionalityReductionValueId);

            builder.HasOne(e => e.Comment)
                   .WithOne(e => e.EmbeddingData)
                   .HasForeignKey<Comment>(e => e.EmbeddingDataId);

            builder.HasMany(e => e.Entities)
                   .WithOne(e => e.EmbeddingData)
                   .HasForeignKey(e => e.EmbeddingDataId);

            builder.HasOne(e => e.OriginalEmbedding)
                   .WithOne(e => e.EmbeddingData)
                   .HasForeignKey<EmbeddingData>(e => e.OriginalEmbeddingId);

            builder.HasOne(e => e.ExternalObject)
                   .WithOne(e => e.EmbeddingData)
                   .HasForeignKey<EmbeddingData>(e => e.ExternalObjectId)
                   .IsRequired(false);
        }
    }
}

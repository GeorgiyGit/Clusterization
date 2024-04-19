using Domain.Entities.Embeddings.DimensionEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Embeddings.DimensionEntities
{
    internal class DimensionTypeBuilder : IEntityTypeConfiguration<DimensionType>
    {
        public void Configure(EntityTypeBuilder<DimensionType> builder)
        {
            builder.HasKey(e => e.DimensionCount);

            builder.HasMany(e => e.EmbeddingModels)
                   .WithOne(e => e.DimensionType)
                   .HasForeignKey(e => e.DimensionTypeId);

            builder.HasMany(e => e.DimensionEmbeddingObjects)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.DimensionType)
                   .HasForeignKey(e => e.DimensionCount);
        }
    }
}

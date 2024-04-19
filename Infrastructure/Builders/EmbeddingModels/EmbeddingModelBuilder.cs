using Domain.Entities.EmbeddingModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.EmbeddingModels
{
    internal class EmbeddingModelBuilder : IEntityTypeConfiguration<EmbeddingModel>
    {
        public void Configure(EntityTypeBuilder<EmbeddingModel> builder)
        {
            builder.HasOne(e => e.DimensionType)
                   .WithMany(e => e.EmbeddingModels)
                   .HasForeignKey(e => e.DimensionTypeId);

            builder.HasMany(e => e.Groups)
                   .WithOne(e => e.EmbeddingModel)
                   .HasForeignKey(e => e.EmbeddingModelId);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.EmbeddingModel)
                   .HasForeignKey(e => e.EmbeddingModelId);

            builder.HasMany(e => e.EmbeddingLoadingStates)
                   .WithOne(e => e.EmbeddingModel)
                   .HasForeignKey(e => e.EmbeddingModelId);
        }
    }
}

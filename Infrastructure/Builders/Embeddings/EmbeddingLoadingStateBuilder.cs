using Domain.Entities.Embeddings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Embeddings
{
    internal class EmbeddingLoadingStateBuilder : IEntityTypeConfiguration<EmbeddingLoadingState>
    {
        public void Configure(EntityTypeBuilder<EmbeddingLoadingState> builder)
        {
            builder.HasOne(e => e.EmbeddingModel)
                   .WithMany(e => e.EmbeddingLoadingStates)
                   .HasForeignKey(e => e.EmbeddingModelId);

            builder.HasOne(e => e.Profile)
                   .WithOne(e => e.EmbeddingLoadingState)
                   .HasForeignKey<EmbeddingLoadingState>(e => e.ProfileId)
                   .IsRequired(false);

            builder.HasOne(e => e.AddPack)
                   .WithMany(e => e.EmbeddingLoadingStates)
                   .HasForeignKey(e => e.AddPackId)
                   .IsRequired(false);
        }
    }
}

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
    internal class DimensionEmbeddingObjectBuilder : IEntityTypeConfiguration<DimensionEmbeddingObject>
    {
        public void Configure(EntityTypeBuilder<DimensionEmbeddingObject> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.DimensionEmbeddingObjects)
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.EmbeddingObjectsGroup)
                   .WithMany(e => e.DimensionEmbeddingObjects)
                   .HasForeignKey(e => e.EmbeddingObjectsGroupId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

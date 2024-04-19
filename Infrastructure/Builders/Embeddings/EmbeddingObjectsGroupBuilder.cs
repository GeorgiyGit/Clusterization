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
    internal class EmbeddingObjectsGroupBuilder : IEntityTypeConfiguration<EmbeddingObjectsGroup>
    {
        public void Configure(EntityTypeBuilder<EmbeddingObjectsGroup> builder)
        {
            builder.HasOne(e => e.DataObject)
                   .WithMany(e => e.EmbeddingObjectsGroups)
                   .HasForeignKey(e => e.DataObjectId);

            builder.HasOne(e => e.DRTechnique)
                   .WithMany(e => e.Groups)
                   .HasForeignKey(e => e.DRTechniqueId);

            builder.HasOne(e => e.EmbeddingModel)
                   .WithMany(e => e.Groups)
                   .HasForeignKey(e => e.EmbeddingModelId);

            builder.HasOne(e => e.Workspace)
                   .WithMany(e => e.EmbeddingObjectsGroups)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.DimensionEmbeddingObjects)
                   .WithOne(e => e.EmbeddingObjectsGroup)
                   .HasForeignKey(e => e.EmbeddingObjectsGroupId);
        }
    }
}

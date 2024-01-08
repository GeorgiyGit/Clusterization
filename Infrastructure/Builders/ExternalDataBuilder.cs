using Domain.Entities.Embeddings;
using Domain.Entities.ExternalData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders
{
    internal class ExternalDataBuilder
    {
        public static void ExternalObjectsBuilder(EntityTypeBuilder<ExternalObject> modelBuilder)
        {
            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithOne(e => e.ExternalObject)
                        .HasForeignKey<ExternalObject>(e => e.EmbeddingDataId)
                        .IsRequired(false);

            modelBuilder.HasMany(e => e.Workspaces)
                        .WithMany(e => e.ExternalObjects);

            modelBuilder.HasMany(e => e.ClusterizationEntities)
                        .WithOne(e => e.ExternalObject)
                        .HasForeignKey(e => e.ExternalObjectId)
                        .IsRequired(false);
        }
    }
}

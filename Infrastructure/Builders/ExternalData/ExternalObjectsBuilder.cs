using Domain.Entities.ExternalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.ExternalData
{
    internal class ExternalObjectsBuilder : IEntityTypeConfiguration<ExternalObject>
    {
        public void Configure(EntityTypeBuilder<ExternalObject> builder)
        {
            builder.HasKey(e => e.FullId);

            builder.HasOne(e => e.EmbeddingData)
                   .WithOne(e => e.ExternalObject)
                   .HasForeignKey<ExternalObject>(e => e.EmbeddingDataId)
                   .IsRequired(false);

            builder.HasMany(e => e.Workspaces)
                   .WithMany(e => e.ExternalObjects);

            builder.HasMany(e => e.ClusterizationEntities)
                   .WithOne(e => e.ExternalObject)
                   .HasForeignKey(e => e.ExternalObjectId)
                   .IsRequired(false);
        }
    }
}

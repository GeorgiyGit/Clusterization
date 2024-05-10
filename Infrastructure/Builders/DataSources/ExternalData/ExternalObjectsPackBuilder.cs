using Domain.Entities.DataSources.ExternalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.DataSources.ExternalData
{
    internal class ExternalObjectsPackBuilder : IEntityTypeConfiguration<ExternalObjectsPack>
    {
        public void Configure(EntityTypeBuilder<ExternalObjectsPack> builder)
        {
            builder.HasMany(e => e.ExternalObjects)
                   .WithOne(e => e.Pack)
                   .HasForeignKey(e => e.PackId);

            builder.HasOne(e => e.Owner)
                   .WithMany(e => e.LoadedExternalObjectsPacks)
                   .HasForeignKey(e => e.OwnerId);
        }
    }
}

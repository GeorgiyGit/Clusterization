using Domain.Entities.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Profiles
{
    internal class ClusterizationDimensionTypeBuilder : IEntityTypeConfiguration<ClusterizationDimensionType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationDimensionType> builder)
        {
            builder.HasKey(e => e.DimensionCount);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.DimensionType)
                   .HasForeignKey(e => e.DimensionCount);

            builder.HasMany(e => e.DimensionValues)
                   .WithOne(e => e.DimensionType)
                   .HasForeignKey(e => e.DimensionTypeId);
        }
    }
}

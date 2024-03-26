using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class ClusterizationDimensionTypeSeeder : IEntityTypeConfiguration<ClusterizationDimensionType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationDimensionType> builder)
        {
            var type1 = new ClusterizationDimensionType()
            {
                DimensionCount = 2
            };
            var type2 = new ClusterizationDimensionType()
            {
                DimensionCount = 3
            };
            var type3 = new ClusterizationDimensionType()
            {
                DimensionCount = 100
            };
            var type4 = new ClusterizationDimensionType()
            {
                DimensionCount = 1536
            };

            builder.HasData(
                type1,
                type2,
                type3,
                type4
                );
        }
    }
}

using Domain.Entities.Clusterization;
using Domain.Entities.Tasks;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class ClusterizationSeeder
    {
        public static void DimensionTypeSeeder(EntityTypeBuilder<ClusterizationDimensionType> modelBuilder)
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

            modelBuilder.HasData(
                type1,
                type2,
                type3,
                type4
                );
        }

        public static void ClusterizationTypeSeeder(EntityTypeBuilder<ClusterizationType> modelBuilder)
        {
            var type1 = new ClusterizationType()
            {
                Id = ClusterizationTypes.Comments,
                Name = "Коментарі"
            };
            var external = new ClusterizationType()
            {
                Id = ClusterizationTypes.External,
                Name = "З файлу"
            };

            modelBuilder.HasData(
                type1,
                external
                );

        }
    }
}

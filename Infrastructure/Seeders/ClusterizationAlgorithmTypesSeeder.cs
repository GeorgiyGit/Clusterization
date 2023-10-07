using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class ClusterizationAlgorithmTypesSeeder
    {
        public static void TypesSeeder(EntityTypeBuilder<ClusterizationAlgorithmType> modelBuilder)
        {
            var kMeans = new ClusterizationAlgorithmType()
            {
                Id = ClusterizationAlgorithmTypes.KMeans,
                Name = "k-means",
                Description = "Впорядкування множини об'єктів у порівняно однорідні групи."
            };

            modelBuilder.HasData(kMeans);
        }
    }
}

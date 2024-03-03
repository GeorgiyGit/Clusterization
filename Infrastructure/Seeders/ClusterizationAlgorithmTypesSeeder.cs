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
                Description = "Arrangement of a set of objects into relatively homogeneous groups."//"Впорядкування множини об'єктів у порівняно однорідні групи."
            };

            var oneCluster = new ClusterizationAlgorithmType()
            {
                Id = ClusterizationAlgorithmTypes.OneCluster,
                Name = "One cluster",
                Description = "Combining all elements into one cluster"//"Об'єднання елементів в один кластер"
            };

            var dbSCAN = new ClusterizationAlgorithmType()
            {
                Id = ClusterizationAlgorithmTypes.DBScan,
                Name = "DBSCAN",
                Description = "Density-Based Spatial Clustering Of Applications With Noise"
            };
            var spectralClustering = new ClusterizationAlgorithmType()
            {
                Id = ClusterizationAlgorithmTypes.SpectralClustering,
                Name = "Spectral Clustering",
                Description = "Spectral clustering is based on the principles of graph theory and linear algebra"//"Cпектральна кластеризація базується на принципах теорії графів і лінійної алгебри"
            };
            var gmm = new ClusterizationAlgorithmType()
            {
                Id = ClusterizationAlgorithmTypes.GaussianMixture,
                Name = "Gaussian Mixture",
                Description = "A clustering method that models the data as a mixture of Gaussian partitions"//"Метод кластеризації, який моделює дані як суміш розділів Гауса"
            };

            modelBuilder.HasData(kMeans, oneCluster, dbSCAN, spectralClustering, gmm);
        }
    }
}

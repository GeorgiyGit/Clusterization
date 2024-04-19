using Domain.Entities.Clusterization.Algorithms;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders
{
    internal class ClusterizationAlgorithmTypeSeeder : IEntityTypeConfiguration<ClusterizationAlgorithmType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationAlgorithmType> builder)
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
                Id = ClusterizationAlgorithmTypes.DBSCAN,
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

            builder.HasData(kMeans, oneCluster, dbSCAN, spectralClustering, gmm);
        }
    }
}

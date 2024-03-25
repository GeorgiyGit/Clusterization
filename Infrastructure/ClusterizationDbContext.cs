using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Customers;
using Domain.Entities.DimensionalityReduction;
using Domain.Entities.Embeddings;
using Domain.Entities.ExternalData;
using Domain.Entities.Tasks;
using Domain.Entities.Youtube;
using Infrastructure.Builders;
using Infrastructure.Builders.Customers;
using Infrastructure.Builders.Youtube;
using Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ClusterizationDbContext : IdentityDbContext<Customer>
    {
        #region Youtube
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        #endregion

        #region Tasks
        public virtual DbSet<MyTask> MyTasks { get; set; }
        public virtual DbSet<MyTaskState> MyTaskStates { get; set; }
        #endregion

        #region Embedding
        public virtual DbSet<EmbeddingData> EmbeddingDatas { get; set; }
        public virtual DbSet<EmbeddingDimensionValue> EmbeddingDimensionValues { get; set; }
        #endregion

        #region Clusterization
        public virtual DbSet<Cluster> Clusters { get; set; }
        public virtual DbSet<ClusterizationDimensionType> ClusterizationDimensionTypes { get; set; }
        public virtual DbSet<ClusterizationEntity> ClusterizationEntites { get; set; }
        public virtual DbSet<ClusterizationProfile> ClusterizationProfiles { get; set; }
        public virtual DbSet<ClusterizationType> ClusterizationTypes { get; set; }
        public virtual DbSet<ClusterizationWorkspace> ClusterizationWorkspaces { get; set; }
        public virtual DbSet<DisplayedPoint> DisplayedPoints { get; set; }
        public virtual DbSet<ClusterizationTile> ClusterizationTiles { get; set; }
        public virtual DbSet<ClusterizationTilesLevel> ClusterizationTilesLevels { get; set; }

        public virtual DbSet<ClusterizationWorkspaceDRTechnique> ClusterizationWorkspaceDRTechniques { get; set; }

        #region Algorithms
        public virtual DbSet<ClusterizationAbstactAlgorithm> ClusterizationAbstractAlgorithms { get; set; }
        public virtual DbSet<KMeansAlgorithm> KMeansAlgorithms { get; set; }
        public virtual DbSet<OneClusterAlgorithm> OneClusterAlgorithms { get; set; }
        public virtual DbSet<DBScanAlgorithm> DBScanAlgorithms { get; set; }
        public virtual DbSet<SpectralClusteringAlgorithm> SpectralClusteringAlgorithms { get; set; }
        public virtual DbSet<GaussianMixtureAlgorithm> GaussianMixtureAlgorithms { get; set; }
        #endregion

        #endregion

        #region DimensionalityReduction
        public virtual DbSet<DimensionalityReductionTechnique> DimensionalityReductionTechniques { get; set; }
        public virtual DbSet<DimensionalityReductionValue> DimensionalityReductionValues { get; set; }
        #endregion

        #region ExternalData
        public virtual DbSet<ExternalObject> ExternalObjects { get; set; }
        #endregion

        public ClusterizationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerBuilder).Assembly);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            MyTaskSeeder.StateSeeder(modelBuilder.Entity<MyTaskState>());
            ClusterizationSeeder.ClusterizationTypeSeeder(modelBuilder.Entity<ClusterizationType>());
            ClusterizationSeeder.DimensionTypeSeeder(modelBuilder.Entity<ClusterizationDimensionType>());
            ClusterizationAlgorithmTypesSeeder.TypesSeeder(modelBuilder.Entity<ClusterizationAlgorithmType>());
            DimensionalityReductionSeeder.TechniquesSeeder(modelBuilder.Entity<DimensionalityReductionTechnique>());
        }
    }
}

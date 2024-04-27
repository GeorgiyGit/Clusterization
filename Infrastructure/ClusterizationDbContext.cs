using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Customers;
using Domain.Entities.Quotas;
using Domain.Entities.Tasks;
using Infrastructure.Builders.Customers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataSources.Youtube;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entitie.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.DimensionalityReductionEntities;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings;
using Domain.Entities.Embeddings.DimensionEntities;
using Domain.Entities.Clusterization.Profiles;

namespace Infrastructure
{
    public class ClusterizationDbContext : IdentityDbContext<Customer>
    {
        #region DataSources

        #region Youtube
        public virtual DbSet<YoutubeComment> YoutubeComments { get; set; }
        public virtual DbSet<YoutubeVideo> YoutubeVideos { get; set; }
        public virtual DbSet<YoutubeChannel> YoutubeChannels { get; set; }
        #endregion

        #region ExternalData
        public virtual DbSet<ExternalObject> ExternalObjects { get; set; }
        #endregion

        #endregion

        #region Tasks
        public virtual DbSet<MyTask> MyTasks { get; set; }
        public virtual DbSet<MyTaskState> MyTaskStates { get; set; }
        #endregion

        #region Quotes
        public virtual DbSet<CustomerQuotas> CustomerQuotas { get; set; }
        public virtual DbSet<QuotasType> QuotasTypes { get; set; }
        public virtual DbSet<QuotasLogs> QuotasLogs { get; set; }

        public virtual DbSet<QuotasPack> QuotasPacks { get; set; }
        public virtual DbSet<QuotasPackItem> QuotasPackItems { get; set; }
        public virtual DbSet<QuotasPackLogs> QuotasPackLogs { get; set; }
        #endregion

        #region Clusterization
        public virtual DbSet<ClusterizationProfile> ClusterizationProfiles { get; set; }
        public virtual DbSet<ClusterizationType> ClusterizationTypes { get; set; }

        public virtual DbSet<ClusterizationWorkspace> ClusterizationWorkspaces { get; set; }
        public virtual DbSet<WorkspaceDataObjectsAddPack> WorkspaceDataObjectsAddPacks { get; set; }

        public virtual DbSet<Cluster> Clusters { get; set; }
        public virtual DbSet<ClusterizationTile> ClusterizationTiles { get; set; }
        public virtual DbSet<ClusterizationTilesLevel> ClusterizationTilesLevels { get; set; }
        public virtual DbSet<DisplayedPoint> DisplayedPoints { get; set; }


        #region Algorithms
        public virtual DbSet<ClusterizationAlgorithmType> ClusterizationAlgorithmTypes { get; set; }
        public virtual DbSet<ClusterizationAbstactAlgorithm> ClusterizationAbstractAlgorithms { get; set; }
        public virtual DbSet<KMeansAlgorithm> KMeansAlgorithms { get; set; }
        public virtual DbSet<OneClusterAlgorithm> OneClusterAlgorithms { get; set; }
        public virtual DbSet<DBSCANAlgorithm> DBSCANAlgorithms { get; set; }
        public virtual DbSet<SpectralClusteringAlgorithm> SpectralClusteringAlgorithms { get; set; }
        public virtual DbSet<GaussianMixtureAlgorithm> GaussianMixtureAlgorithms { get; set; }
        #endregion

        #endregion

        #region DimensionalityReduction
        public virtual DbSet<DimensionalityReductionTechnique> DimensionalityReductionTechniques { get; set; }
        #endregion

        #region EmbeddingModels
        public virtual DbSet<EmbeddingModel> EmbeddingModels { get; set; }
        #endregion

        #region Embeddings
        public virtual DbSet<DimensionEmbeddingObject> DimensionEmbeddingObjects { get; set; }
        public virtual DbSet<DimensionType> DimensionTypes { get; set; }

        public virtual DbSet<EmbeddingLoadingState> EmbeddingLoadingStates { get; set; }
        public virtual DbSet<EmbeddingObjectsGroup> EmbeddingObjectsGroups { get; set; }
        #endregion

        public ClusterizationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerBuilder).Assembly);
        }
    }
}

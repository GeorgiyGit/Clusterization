using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Embeddings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders
{
    internal class ClusterizationBuilder
    {
        public static void BuildAll(ModelBuilder modelBuilder)
        {
            ClusterBuild(modelBuilder.Entity<Cluster>());
            ColorValueBuild(modelBuilder.Entity<ClusterizationColorValue>());
            DimensionTypeBuild(modelBuilder.Entity<ClusterizationDimensionType>());
            EntityBuild(modelBuilder.Entity<ClusterizationEntity>());
            PointColorsBuild(modelBuilder.Entity<ClusterizationPointColors>());
            ProfileBuild(modelBuilder.Entity<ClusterizationProfile>());
            ClusterTypeBuild(modelBuilder.Entity<ClusterizationType>());
            WorkspaceBuild(modelBuilder.Entity<ClusterizationWorkspace>());
            DisplayedPointBuild(modelBuilder.Entity<DisplayedPoint>());
            TileBuild(modelBuilder.Entity<ClusterizationTile>());
        }
        public static void ClusterBuild(EntityTypeBuilder<Cluster> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Color)
                        .WithOne(e => e.Cluster)
                        .HasForeignKey<ClusterizationColorValue>(e => e.ClusterId);

            modelBuilder.HasMany(e => e.Entities)
                        .WithMany(e => e.Clusters);

            modelBuilder.HasOne(e => e.Profile)
                        .WithMany(e => e.Clusters)
                        .HasForeignKey(e => e.ProfileId);
        }
        public static void ColorValueBuild(EntityTypeBuilder<ClusterizationColorValue> modelBuilder)
        {
            modelBuilder.HasOne(e => e.PointColors)
                        .WithMany(e => e.Colors)
                        .HasForeignKey(e => e.PointColorsId)
                        .IsRequired(false);

            modelBuilder.HasOne(e => e.Cluster)
                        .WithOne(e => e.Color)
                        .HasForeignKey<Cluster>(e => e.ColorId)
                        .IsRequired(false);
        }
        public static void DimensionTypeBuild(EntityTypeBuilder<ClusterizationDimensionType> modelBuilder)
        {
            modelBuilder.HasKey(e => e.DimensionCount);

            modelBuilder.HasMany(e => e.Profiles)
                        .WithOne(e => e.DimensionType)
                        .HasForeignKey(e => e.DimensionTypeId);

            modelBuilder.HasMany(e => e.DimensionValues)
                        .WithOne(e => e.DimensionType)
                        .HasForeignKey(e => e.DimensionTypeId);
        }
        public static void EntityBuild(EntityTypeBuilder<ClusterizationEntity> modelBuilder)
        {
            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithMany(e => e.Entities)
                        .HasForeignKey(e => e.EmbeddingDataId)
                        .IsRequired(false);

            modelBuilder.HasOne(e => e.Comment)
                        .WithMany(e => e.ClusterizationEntities)
                        .HasForeignKey(e => e.CommentId);

            modelBuilder.HasOne(e => e.DisplayedPoint)
                        .WithOne(e => e.ClusterizationEntity)
                        .HasForeignKey<DisplayedPoint>(e => e.ClusterizationEntityId)
                        .IsRequired(false);

            modelBuilder.HasMany(e => e.Clusters)
                        .WithMany(e => e.Entities);

            modelBuilder.HasOne(e => e.Workspace)
                        .WithMany(e => e.Entities)
                        .HasForeignKey(e => e.WorkspaceId);
        }
        public static void PointColorsBuild(EntityTypeBuilder<ClusterizationPointColors> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Point)
                        .WithMany(e => e.Colors)
                        .HasForeignKey(e => e.PointId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasOne(e => e.Profile)
                        .WithMany(e => e.PointColorsCollection)
                        .HasForeignKey(e => e.ProfileId);

            modelBuilder.HasMany(e => e.Colors)
                        .WithOne(e => e.PointColors)
                        .HasForeignKey(e => e.PointColorsId);
        }
        public static void ProfileBuild(EntityTypeBuilder<ClusterizationProfile> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Algorithm)
                        .WithMany(e => e.Profiles)
                        .HasForeignKey(e => e.AlgorithmId);

            modelBuilder.HasOne(e => e.DimensionType)
                        .WithMany(e => e.Profiles)
                        .HasForeignKey(e => e.DimensionTypeId);

            modelBuilder.HasOne(e => e.Workspace)
                        .WithMany(e => e.Profiles)
                        .HasForeignKey(e => e.WorkspaceId);

            modelBuilder.HasMany(e => e.Clusters)
                        .WithOne(e => e.Profile)
                        .HasForeignKey(e => e.ProfileId);

            modelBuilder.HasMany(e => e.PointColorsCollection)
                        .WithOne(e => e.Profile)
                        .HasForeignKey(e => e.ProfileId);

            modelBuilder.HasMany(e => e.Tiles)
                        .WithOne(e => e.Profile)
                        .HasForeignKey(e => e.ProfileId);
        }
        public static void ClusterTypeBuild(EntityTypeBuilder<ClusterizationType> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Workspaces)
                        .WithOne(e => e.Type)
                        .HasForeignKey(e => e.TypeId);
        }
        public static void WorkspaceBuild(EntityTypeBuilder<ClusterizationWorkspace> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Type)
                        .WithMany(e => e.Workspaces)
                        .HasForeignKey(e => e.TypeId);

            modelBuilder.HasMany(e => e.Comments)
                        .WithMany(e => e.Workspaces);

            modelBuilder.HasMany(e => e.Profiles)
                        .WithOne(e => e.Workspace)
                        .HasForeignKey(e => e.WorkspaceId);

            modelBuilder.HasMany(e => e.Entities)
                        .WithOne(e => e.Workspace)
                        .HasForeignKey(e => e.WorkspaceId);
        }
        public static void DisplayedPointBuild(EntityTypeBuilder<DisplayedPoint> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Tile)
                        .WithMany(e => e.Points)
                        .HasForeignKey(e => e.TileId);

            modelBuilder.HasMany(e => e.Colors)
                        .WithOne(e => e.Point)
                        .HasForeignKey(e => e.PointId);

            modelBuilder.HasMany(e => e.Points)
                        .WithOne(e => e.ParentPoint)
                        .HasForeignKey(e => e.ParentPointId)
                        .IsRequired(false);

            modelBuilder.HasOne(e => e.ClusterizationEntity)
                        .WithOne(e => e.DisplayedPoint)
                        .HasForeignKey<ClusterizationEntity>(e => e.DisplayedPointId)
                        .IsRequired(false);
        }
        public static void TileBuild(EntityTypeBuilder<ClusterizationTile> modelBuilder)
        {
            modelBuilder.HasMany(e => e.ChildTiles)
                        .WithOne(e => e.Parent)
                        .HasForeignKey(e => e.ParentId)
                        .IsRequired(false);

            modelBuilder.HasMany(e => e.Points)
                        .WithOne(e => e.Tile)
                        .HasForeignKey(e => e.TileId);

            modelBuilder.HasOne(e => e.Profile)
                        .WithMany(e => e.Tiles)
                        .HasForeignKey(e => e.ProfileId);
        }
    }
}

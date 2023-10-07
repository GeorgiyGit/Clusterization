using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms;
using Google.Apis.YouTube.v3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders
{
    internal class ClusterizationAlgorithmsBuilder
    {
        public static void BuildAll(ModelBuilder modelBuilder)
        {
            AlgorithmTypeBuild(modelBuilder.Entity<ClusterizationAlgorithmType>());
            AbstactAlgorithmBuild(modelBuilder.Entity<ClusterizationAbstactAlgorithm>());
        }
        public static void AlgorithmTypeBuild(EntityTypeBuilder<ClusterizationAlgorithmType> modelBuilder)
        {
            modelBuilder.HasMany(e => e.ClusterizationAlgorithms)
                        .WithOne(e => e.Type)
                        .HasForeignKey(e => e.TypeId);
        }
        public static void AbstactAlgorithmBuild(EntityTypeBuilder<ClusterizationAbstactAlgorithm> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Profiles)
                        .WithOne(e => e.Algorithm)
                        .HasForeignKey(e => e.AlgorithmId);

            modelBuilder.HasOne(e => e.Type)
                        .WithMany(e => e.ClusterizationAlgorithms)
                        .HasForeignKey(e => e.TypeId);
        }
    }
}

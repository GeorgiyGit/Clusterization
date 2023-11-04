using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
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
    internal class DimensionalityReductionBuilder
    {
        public static void BuildAll(ModelBuilder modelBuilder)
        {
            TechniqueBuild(modelBuilder.Entity<DimensionalityReductionTechnique>());
            ValueBuild(modelBuilder.Entity<DimensionalityReductionValue>());
        }

        public static void TechniqueBuild(EntityTypeBuilder<DimensionalityReductionTechnique> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Values)
                        .WithOne(e => e.Technique)
                        .HasForeignKey(e => e.TechniqueId);

            modelBuilder.HasMany(e => e.Profiles)
                        .WithOne(e => e.DimensionalityReductionTechnique)
                        .HasForeignKey(e => e.DimensionalityReductionTechniqueId);
        }

        public static void ValueBuild(EntityTypeBuilder<DimensionalityReductionValue> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Embeddings)
                        .WithOne(e => e.DimensionalityReductionValue)
                        .HasForeignKey(e => e.DimensionalityReductionValueId);

            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithOne(e => e.DimensionalityReductionValue)
                        .HasForeignKey<DimensionalityReductionValue>(e => e.EmbeddingDataId);

            modelBuilder.HasOne(e => e.Technique)
                        .WithMany(e => e.Values)
                        .HasForeignKey(e => e.TechniqueId);

            modelBuilder.HasOne(e => e.ClusterizationEntity)
                        .WithMany(e => e.DimensionalityReductionValues)
                        .HasForeignKey(e => e.ClusterizationEntityId);
        }
    }
}

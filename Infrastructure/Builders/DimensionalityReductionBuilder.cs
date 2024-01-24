﻿using Domain.Entities.Clusterization;
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

            modelBuilder.HasMany(e => e.ClusterizationWorkspaceDRTechniques)
                        .WithOne(e => e.DRTechnique)
                        .HasForeignKey(e => e.DRTechniqueId);
        }

        public static void ValueBuild(EntityTypeBuilder<DimensionalityReductionValue> modelBuilder)
        {
            modelBuilder.HasMany(e => e.Embeddings)
                        .WithOne(e => e.DimensionalityReductionValue)
                        .HasForeignKey(e => e.DimensionalityReductionValueId);

            modelBuilder.HasIndex(e => new { e.TechniqueId, e.ClusterizationWorkspaceDRTechniqueId });

            modelBuilder.HasOne(e => e.EmbeddingData)
                        .WithOne(e => e.DimensionalityReductionValue)
                        .HasForeignKey<DimensionalityReductionValue>(e => e.EmbeddingDataId);

            modelBuilder.HasOne(e => e.Technique)
                        .WithMany(e => e.Values)
                        .HasForeignKey(e => e.TechniqueId);

            modelBuilder.HasOne(e => e.ClusterizationEntity)
                        .WithMany(e => e.DimensionalityReductionValues)
                        .HasForeignKey(e => e.ClusterizationEntityId);

            modelBuilder.HasOne(e => e.ClusterizationWorkspaceDRTechnique)
                        .WithMany(e => e.DRValues)
                        .HasForeignKey(e => e.ClusterizationWorkspaceDRTechniqueId)
                        .IsRequired(false);
        }
    }
}

using Domain.Entities.Customers;
using Domain.Entities.DimensionalityReduction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.DimensionalityReduction
{
    internal class DimensionalityReductionTechniqueBuilder : IEntityTypeConfiguration<DimensionalityReductionTechnique>
    {
        public void Configure(EntityTypeBuilder<DimensionalityReductionTechnique> builder)
        {
            builder.HasMany(e => e.Values)
                   .WithOne(e => e.Technique)
                   .HasForeignKey(e => e.TechniqueId);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.DimensionalityReductionTechnique)
                   .HasForeignKey(e => e.DimensionalityReductionTechniqueId);

            builder.HasMany(e => e.ClusterizationWorkspaceDRTechniques)
                   .WithOne(e => e.DRTechnique)
                   .HasForeignKey(e => e.DRTechniqueId);
        }
    }
}

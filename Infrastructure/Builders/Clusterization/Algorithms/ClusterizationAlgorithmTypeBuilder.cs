using Domain.Entities.Clusterization.Algorithms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Algorithms
{
    internal class ClusterizationAlgorithmTypeBuilder : IEntityTypeConfiguration<ClusterizationAlgorithmType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationAlgorithmType> builder)
        {
            builder.HasMany(e => e.ClusterizationAlgorithms)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}

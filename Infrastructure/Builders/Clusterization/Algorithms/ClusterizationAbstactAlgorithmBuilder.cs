using Domain.Entities.Clusterization.Algorithms;
using Hangfire.Common;
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
    internal class ClusterizationAbstactAlgorithmBuilder : IEntityTypeConfiguration<ClusterizationAbstactAlgorithm>
    {
        public void Configure(EntityTypeBuilder<ClusterizationAbstactAlgorithm> builder)
        {
            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.Algorithm)
                   .HasForeignKey(e => e.AlgorithmId);

            builder.HasOne(e => e.Type)
                   .WithMany(e => e.ClusterizationAlgorithms)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}

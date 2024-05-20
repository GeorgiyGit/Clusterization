using Domain.Entities.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization
{
    internal class ClusterBuilder : IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {
            builder.HasMany(e => e.ChildClusters)
                   .WithOne(e => e.ParentCluster)
                   .HasForeignKey(e => e.ParentClusterId)
                   .IsRequired(false);

            builder.HasMany(e => e.DataObjects)
                   .WithMany(e => e.Clusters);

            builder.HasOne(e => e.Profile)
                   .WithMany(e => e.Clusters)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasMany(e => e.DisplayedPoints)
                   .WithOne(e => e.Cluster)
                   .HasForeignKey(e => e.ClusterId);

            builder.HasIndex(e => new { e.ChildElementsCount });
        }
    }
}

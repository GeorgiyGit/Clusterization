using Domain.Entities.Clusterization.FastClustering;
using Domain.Entities.Clusterization.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.FastClustering
{
    public class FastClusteringWorkflowBuilder : IEntityTypeConfiguration<FastClusteringWorkflow>
    {
        public void Configure(EntityTypeBuilder<FastClusteringWorkflow> builder)
        {
            builder.HasOne(e => e.Owner)
                   .WithOne(e => e.FastClusteringWorkflow)
                   .HasForeignKey<FastClusteringWorkflow>(e => e.OwnerId);

            builder.HasMany(e => e.Workspaces)
                   .WithOne(e => e.FastClusteringWorkflow)
                   .HasForeignKey(e => e.FastClusteringWorkflowId)
                   .IsRequired(false);
        }
    }
}

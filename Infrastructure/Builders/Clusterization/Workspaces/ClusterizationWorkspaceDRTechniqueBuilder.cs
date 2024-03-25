using Domain.Entities.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Workspaces
{
    internal class ClusterizationWorkspaceDRTechniqueBuilder : IEntityTypeConfiguration<ClusterizationWorkspaceDRTechnique>
    {
        public void Configure(EntityTypeBuilder<ClusterizationWorkspaceDRTechnique> builder)
        {
            builder.HasOne(e => e.DRTechnique)
                   .WithMany(e => e.ClusterizationWorkspaceDRTechniques)
                   .HasForeignKey(e => e.DRTechniqueId);

            builder.HasOne(e => e.Workspace)
                   .WithMany(e => e.ClusterizationWorkspaceDRTechniques)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.DRValues)
                   .WithOne(e => e.ClusterizationWorkspaceDRTechnique)
                   .HasForeignKey(e => e.ClusterizationWorkspaceDRTechniqueId)
                   .IsRequired(false);
        }
    }
}

using Domain.Entities.Clusterization.Workspaces;
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
    internal class ClusterizationWorkspaceBuilder : IEntityTypeConfiguration<ClusterizationWorkspace>
    {
        public void Configure(EntityTypeBuilder<ClusterizationWorkspace> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.Workspaces)
                   .HasForeignKey(e => e.TypeId);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.Workspace)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasOne(e => e.Owner)
                   .WithMany(e => e.Workspaces)
                   .HasForeignKey(e => e.OwnerId);

            builder.HasMany(e => e.EmbeddingObjectsGroups)
                   .WithOne(e => e.Workspace)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.DataObjects)
                   .WithMany(e => e.Workspaces);

            builder.HasMany(e => e.WorkspaceDataObjectsAddPacks)
                   .WithOne(e => e.Workspace)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasOne(e => e.FastClusteringWorkflow)
                   .WithMany(e => e.Workspaces)
                   .HasForeignKey(e => e.FastClusteringWorkflowId)
                   .IsRequired(false);
        }
    }
}

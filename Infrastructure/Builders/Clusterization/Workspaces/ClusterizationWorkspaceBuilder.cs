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
    internal class ClusterizationWorkspaceBuilder : IEntityTypeConfiguration<ClusterizationWorkspace>
    {
        public void Configure(EntityTypeBuilder<ClusterizationWorkspace> builder)
        {
            builder.HasOne(e => e.Type)
                   .WithMany(e => e.Workspaces)
                   .HasForeignKey(e => e.TypeId);

            builder.HasMany(e => e.Comments)
                   .WithMany(e => e.Workspaces);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.Workspace)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.Entities)
                   .WithOne(e => e.Workspace)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.ClusterizationWorkspaceDRTechniques)
                   .WithOne(e => e.Workspace)
                   .HasForeignKey(e => e.WorkspaceId);

            builder.HasMany(e => e.ExternalObjects)
                   .WithMany(e => e.Workspaces);

            builder.HasOne(e => e.Owner)
                   .WithMany(e => e.Workspaces)
                   .HasForeignKey(e => e.OwnerId);
        }
    }
}

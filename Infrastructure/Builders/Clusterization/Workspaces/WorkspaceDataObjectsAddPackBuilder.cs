using Domain.Entities.Clusterization.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Workspaces
{
    internal class WorkspaceDataObjectsAddPackBuilder : IEntityTypeConfiguration<WorkspaceDataObjectsAddPack>
    {
        public void Configure(EntityTypeBuilder<WorkspaceDataObjectsAddPack> builder)
        {
            builder.HasMany(e => e.DataObjects)
                   .WithMany(e => e.WorkspaceDataObjectsAddPacks);

            builder.HasOne(e => e.Workspace)
                   .WithMany(e => e.WorkspaceDataObjectsAddPacks)
                   .HasForeignKey(e => e.WorkspaceId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Owner)
                   .WithMany(e => e.WorkspaceDataObjectsAddPacks)
                   .HasForeignKey(e => e.OwnerId);

            builder.HasMany(e => e.EmbeddingLoadingStates)
                   .WithOne(e => e.AddPack)
                   .HasForeignKey(e => e.AddPackId)
                   .IsRequired(false);
        }
    }
}

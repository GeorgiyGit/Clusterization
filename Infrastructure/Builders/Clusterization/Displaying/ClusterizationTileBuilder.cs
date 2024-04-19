using Domain.Entities.Customers;
using Domain.Entities.Clusterization.Displaying;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Displaying
{
    internal class ClusterizationTileBuilder : IEntityTypeConfiguration<ClusterizationTile>
    {
        public void Configure(EntityTypeBuilder<ClusterizationTile> builder)
        {
            builder.HasMany(e => e.ChildTiles)
                   .WithOne(e => e.Parent)
                   .HasForeignKey(e => e.ParentId)
                   .IsRequired(false);

            builder.HasMany(e => e.Points)
                   .WithOne(e => e.Tile)
                   .HasForeignKey(e => e.TileId);

            builder.HasOne(e => e.Profile)
                   .WithMany(e => e.Tiles)
                   .HasForeignKey(e => e.ProfileId);

            builder.HasOne(e => e.TilesLevel)
                   .WithMany(e => e.Tiles)
                   .HasForeignKey(e => e.TilesLevelId);
        }
    }
}

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
    internal class DisplayedPointBuilder : IEntityTypeConfiguration<DisplayedPoint>
    {
        public void Configure(EntityTypeBuilder<DisplayedPoint> builder)
        {
            builder.HasOne(e => e.Tile)
                   .WithMany(e => e.Points)
                   .HasForeignKey(e => e.TileId);

            builder.HasOne(e => e.Cluster)
                   .WithMany(e => e.DisplayedPoints)
                   .HasForeignKey(e => e.ClusterId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.DataObject)
                   .WithMany(e => e.DisplayedPoints)
                   .HasForeignKey(e => e.DataObjectId);
        }
    }
}

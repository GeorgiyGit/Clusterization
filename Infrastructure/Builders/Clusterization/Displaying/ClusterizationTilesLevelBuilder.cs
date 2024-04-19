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
    internal class ClusterizationTilesLevelBuilder : IEntityTypeConfiguration<ClusterizationTilesLevel>
    {
        public void Configure(EntityTypeBuilder<ClusterizationTilesLevel> builder)
        {
            builder.HasMany(e => e.Tiles)
                   .WithOne(e => e.TilesLevel)
                   .HasForeignKey(e => e.TilesLevelId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Profile)
                   .WithMany(e => e.TilesLevels)
                   .HasForeignKey(e => e.ProfileId);
        }
    }
}

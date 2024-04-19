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
    internal class ClusterizationTypeBuilder : IEntityTypeConfiguration<ClusterizationType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationType> builder)
        {
            builder.HasMany(e => e.Workspaces)
                   .WithOne(e => e.Type)
                   .HasForeignKey(e => e.TypeId);
        }
    }
}

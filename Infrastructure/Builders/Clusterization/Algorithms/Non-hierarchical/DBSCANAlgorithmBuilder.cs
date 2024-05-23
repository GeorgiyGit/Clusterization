using Domain.Entitie.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Algorithms.Non_hierarchical
{
    internal class DBSCANAlgorithmBuilder : IEntityTypeConfiguration<DBSCANAlgorithm>
    {
        public void Configure(EntityTypeBuilder<DBSCANAlgorithm> builder)
        {
            builder.HasIndex(e => e.Epsilon);
        }
    }
}

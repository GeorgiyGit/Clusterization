﻿using Domain.Entitie.Clusterization.Algorithms.Non_hierarchical;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builders.Clusterization.Algorithms.Non_hierarchical
{
    internal class KMeansAlgorithmBuilder : IEntityTypeConfiguration<KMeansAlgorithm>
    {
        public void Configure(EntityTypeBuilder<KMeansAlgorithm> builder)
        {
            builder.HasIndex(e => e.NumClusters);
        }
    }
}

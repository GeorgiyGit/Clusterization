﻿using Domain.Entities.Clusterization;
using Domain.Entities.DimensionalityReduction;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class DimensionalityReductionSeeder
    {
        public static void TechniquesSeeder(EntityTypeBuilder<DimensionalityReductionTechnique> modelBuilder)
        {
            var PCA = new DimensionalityReductionTechnique()
            {
                Id = DimensionalityReductionTechniques.PCA,
                Name = "Principal Component Analysis"
            };
            var tSNE = new DimensionalityReductionTechnique()
            {
                Id = DimensionalityReductionTechniques.t_SNE,
                Name = "t-Distributed Stochastic Neighbor Embedding"
            };
            //var MDS = new DimensionalityReductionTechnique()
            //{
                //Id = DimensionalityReductionTechniques.MDS,
                //Name = "Multi-Dimensional Scaling"
            //};
            //var isomap = new DimensionalityReductionTechnique()
            //{
                //Id = DimensionalityReductionTechniques.Isomap,
                //Name = "Isomap"
            //};
            //var LLE = new DimensionalityReductionTechnique()
            //{
                //Id = DimensionalityReductionTechniques.LLE,
                //Name = "Locally Linear Embedding"
            //};
            var JSL = new DimensionalityReductionTechnique()
            {
                Id = DimensionalityReductionTechniques.JSL,
                Name = "Johnson-Lindenstrauss lemma"
            };
            //var LDA = new DimensionalityReductionTechnique()
            //{
               //Id = DimensionalityReductionTechniques.LDA,
               //Name = "Linear Discriminant Analysis"
            //};

            modelBuilder.HasData(PCA,
                                 tSNE,
                                 //MDS,
                                 //isomap,
                                 //LLE,
                                 JSL);
        }
    }
}

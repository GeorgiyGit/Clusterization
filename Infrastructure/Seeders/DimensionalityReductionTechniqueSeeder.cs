using Domain.Entities.DimensionalityReductionEntities;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders
{
    internal class DimensionalityReductionTechniqueSeeder : IEntityTypeConfiguration<DimensionalityReductionTechnique>
    {
        public void Configure(EntityTypeBuilder<DimensionalityReductionTechnique> builder)
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

            builder.HasData(PCA,
                            tSNE,
                            //MDS,
                            //isomap,
                             //LLE,
                             JSL);
        }
    }
}

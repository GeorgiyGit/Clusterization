using Domain.Entities.DimensionalityReductionEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Builders.DimensionalityReduction
{
    internal class DimensionalityReductionTechniqueBuilder : IEntityTypeConfiguration<DimensionalityReductionTechnique>
    {
        public void Configure(EntityTypeBuilder<DimensionalityReductionTechnique> builder)
        {
            builder.HasMany(e => e.Groups)
                   .WithOne(e => e.DRTechnique)
                   .HasForeignKey(e => e.DRTechniqueId);

            builder.HasMany(e => e.Profiles)
                   .WithOne(e => e.DRTechnique)
                   .HasForeignKey(e => e.DRTechniqueId);
        }
    }
}

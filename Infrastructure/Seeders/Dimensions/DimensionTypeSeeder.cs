using Domain.Entities.Embeddings.DimensionEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders.Dimensions
{
    internal class DimensionTypeSeeder : IEntityTypeConfiguration<DimensionType>
    {
        public void Configure(EntityTypeBuilder<DimensionType> builder)
        {
            var type1 = new DimensionType()
            {
                DimensionCount = 2
            };
            var type2 = new DimensionType()
            {
                DimensionCount = 3
            };
            var type3 = new DimensionType()
            {
                DimensionCount = 100
            };
            var type4 = new DimensionType()
            {
                DimensionCount = 1536
            };
            var type5 = new DimensionType()
            {
                DimensionCount = 3072
            };

            builder.HasData(
                type1,
                type2,
                type3,
                type4,
                type5
                );
        }
    }
}

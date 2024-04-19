using Domain.Entities.Clusterization;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders
{
    internal class ClusterizationTypeSeeder : IEntityTypeConfiguration<ClusterizationType>
    {
        public void Configure(EntityTypeBuilder<ClusterizationType> builder)
        {
            var type1 = new ClusterizationType()
            {
                Id = ClusterizationTypes.Comments,
                Name = "Comments"//"Коментарі"
            };
            var external = new ClusterizationType()
            {
                Id = ClusterizationTypes.External,
                Name = "From file"//"З файлу"
            };

            builder.HasData(
                type1,
                external
                );
        }
    }
}

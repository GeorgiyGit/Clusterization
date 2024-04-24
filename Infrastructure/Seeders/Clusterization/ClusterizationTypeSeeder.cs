using Domain.Entities.Clusterization;
using Domain.Resources.Types;
using Domain.Resources.Types.Clusterization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders.Clusterization
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

using Domain.Entities.Quotas;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders
{
    internal class QuotasTypeSeeder : IEntityTypeConfiguration<QuotasType>
    {
        public void Configure(EntityTypeBuilder<QuotasType> builder)
        {
            var youtube = new QuotasType()
            {
                Id = QuotesTypes.Youtube,
                Name = "Youtube",
                Description = "Loading data from Youtube"
            };

            var embeddings = new QuotasType()
            {
                Id = QuotesTypes.Embeddings,
                Name = "Embeddings",
                Description = "Creating embeddings"
            };

            var clustering = new QuotasType()
            {
                Id = QuotesTypes.Clustering,
                Name = "Clustering",
                Description = "Clusterization of data"
            };

            var publicWorkspaces = new QuotasType()
            {
                Id = QuotesTypes.PublicWorkspaces,
                Name = "Public workspaces",
                Description = "Creating public workspaces"
            };
            var privateWorkspaces = new QuotasType()
            {
                Id = QuotesTypes.PrivateWorkspaces,
                Name = "Private workspaces",
                Description = "Creating private workspaces"
            };

            var publicProfiles = new QuotasType()
            {
                Id = QuotesTypes.PublicProfiles,
                Name = "Public profiles",
                Description = "Creating public profiles"
            };
            var privateProfiles = new QuotasType()
            {
                Id = QuotesTypes.PrivateProfiles,
                Name = "Private profiles",
                Description = "Creating private profiles"
            };

            builder.HasData(youtube,
                embeddings,
                clustering,
                publicWorkspaces,
                privateWorkspaces,
                publicProfiles,
                privateProfiles);
        }
    }
}

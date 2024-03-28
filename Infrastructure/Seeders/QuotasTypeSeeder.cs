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
                Id = QuotasTypes.Youtube,
                Name = "Youtube",
                Description = "Loading data from Youtube"
            };

            var embeddings = new QuotasType()
            {
                Id = QuotasTypes.Embeddings,
                Name = "Embeddings",
                Description = "Creating embeddings"
            };

            var clustering = new QuotasType()
            {
                Id = QuotasTypes.Clustering,
                Name = "Clustering",
                Description = "Clusterization of data"
            };

            var publicWorkspaces = new QuotasType()
            {
                Id = QuotasTypes.PublicWorkspaces,
                Name = "Public workspaces",
                Description = "Creating public workspaces"
            };
            var privateWorkspaces = new QuotasType()
            {
                Id = QuotasTypes.PrivateWorkspaces,
                Name = "Private workspaces",
                Description = "Creating private workspaces"
            };

            var publicProfiles = new QuotasType()
            {
                Id = QuotasTypes.PublicProfiles,
                Name = "Public profiles",
                Description = "Creating public profiles"
            };
            var privateProfiles = new QuotasType()
            {
                Id = QuotasTypes.PrivateProfiles,
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

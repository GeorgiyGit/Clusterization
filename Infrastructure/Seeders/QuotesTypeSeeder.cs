using Domain.Entities.Quotes;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class QuotesTypeSeeder : IEntityTypeConfiguration<QuotesType>
    {
        public void Configure(EntityTypeBuilder<QuotesType> builder)
        {
            var youtube = new QuotesType()
            {
                Id = QuotesTypes.Youtube,
                Name = "Youtube",
                Description = "Loading data from Youtube"
            };

            var embeddings = new QuotesType()
            {
                Id = QuotesTypes.Embeddings,
                Name = "Embeddings",
                Description = "Creating embeddings"
            };

            var clustering = new QuotesType()
            {
                Id = QuotesTypes.Clustering,
                Name = "Clustering",
                Description = "Clusterization of data"
            };

            var publicWorkspaces = new QuotesType()
            {
                Id = QuotesTypes.PublicWorkspaces,
                Name = "Public workspaces",
                Description = "Creating public workspaces"
            };
            var privateWorkspaces = new QuotesType()
            {
                Id = QuotesTypes.PrivateWorkspaces,
                Name = "Private workspaces",
                Description = "Creating private workspaces"
            };

            var publicProfiles = new QuotesType()
            {
                Id = QuotesTypes.PublicProfiles,
                Name = "Public profiles",
                Description = "Creating public profiles"
            };
            var privateProfiles = new QuotesType()
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

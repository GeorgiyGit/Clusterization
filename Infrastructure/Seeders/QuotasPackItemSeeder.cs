using Domain.Entities.Quotas;
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
    internal class QuotasPackItemSeeder : IEntityTypeConfiguration<QuotasPackItem>
    {
        public void Configure(EntityTypeBuilder<QuotasPackItem> builder)
        {
            #region basicPack
            //channel - 50
            //video - 10
            //comment - 1
            var basicPackYoutube = new QuotasPackItem()
            {
                Id = 1,
                TypeId = QuotasTypes.Youtube,
                PackId = 1,
                Count = 1000
            };

            //embedding - 1
            var basicPackEmbeddings = new QuotasPackItem()
            {
                Id = 2,
                TypeId = QuotasTypes.Embeddings,
                PackId = 1,
                Count = 1000
            };

            //depending on the context
            var basicPackClustering = new QuotasPackItem()
            {
                Id = 3,
                TypeId = QuotasTypes.Clustering,
                PackId = 1,
                Count = 10_000
            };

            //public workspace - 1
            var basicPackPublicWorkspaces = new QuotasPackItem()
            {
                Id = 4,
                TypeId = QuotasTypes.PublicWorkspaces,
                PackId = 1,
                Count = 5
            };

            //private workspace - 1
            var basicPackPrivateWorkspaces = new QuotasPackItem()
            {
                Id = 5,
                TypeId = QuotasTypes.PrivateWorkspaces,
                PackId = 1,
                Count = 20
            };


            //public profiles - 1
            var basicPackPublicProfiles = new QuotasPackItem()
            {
                Id = 6,
                TypeId = QuotasTypes.PublicProfiles,
                PackId = 1,
                Count = 20
            };

            //private profiles - 1
            var basicPackPrivateProfiles = new QuotasPackItem()
            {
                Id = 7,
                TypeId = QuotasTypes.PrivateProfiles,
                PackId = 1,
                Count = 50
            };
            #endregion

            #region superPack
            //channel - 50
            //video - 10
            //comment - 1
            var superPackYoutube = new QuotasPackItem()
            {
                Id = 8,
                TypeId = QuotasTypes.Youtube,
                PackId = 2,
                Count = 1_000_000_000
            };

            //embedding - 1
            var superPackEmbeddings = new QuotasPackItem()
            {
                Id = 9,
                TypeId = QuotasTypes.Embeddings,
                PackId = 2,
                Count = 1_000_000_000
            };

            //depending on the context
            var superPackClustering = new QuotasPackItem()
            {
                Id = 10,
                TypeId = QuotasTypes.Clustering,
                PackId = 2,
                Count = 1_000_000_000
            };

            //public workspace - 1
            var superPackPublicWorkspaces = new QuotasPackItem()
            {
                Id = 11,
                TypeId = QuotasTypes.PublicWorkspaces,
                PackId = 2,
                Count = 1_000_000_000
            };

            //private workspace - 1
            var superPackPrivateWorkspaces = new QuotasPackItem()
            {
                Id = 12,
                TypeId = QuotasTypes.PrivateWorkspaces,
                PackId = 2,
                Count = 1_000_000_000
            };


            //public profiles - 1
            var superPackPublicProfiles = new QuotasPackItem()
            {
                Id = 13,
                TypeId = QuotasTypes.PublicProfiles,
                PackId = 2,
                Count = 1_000_000_000
            };

            //private profiles - 1
            var superPackPrivateProfiles = new QuotasPackItem()
            {
                Id = 14,
                TypeId = QuotasTypes.PrivateProfiles,
                PackId = 2,
                Count = 1_000_000_000
            };
            #endregion

            builder.HasData(basicPackYoutube,
                            basicPackEmbeddings,
                            basicPackClustering,
                            basicPackPublicWorkspaces,
                            basicPackPrivateWorkspaces,
                            basicPackPublicProfiles,
                            basicPackPrivateProfiles,
                            superPackYoutube,
                            superPackEmbeddings,
                            superPackClustering,
                            superPackPublicWorkspaces,
                            superPackPrivateWorkspaces,
                            superPackPublicProfiles,
                            superPackPrivateProfiles);
        }
    }
}

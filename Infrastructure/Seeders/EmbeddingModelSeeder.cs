using Domain.Entities.EmbeddingModels;
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
    internal class EmbeddingModelSeeder : IEntityTypeConfiguration<EmbeddingModel>
    {
        public void Configure(EntityTypeBuilder<EmbeddingModel> builder)
        {
            var textEmbeddingAda002 = new EmbeddingModel()
            {
                Id = EmbeddingModelList.text_embedding_ada_002,
                MaxInputCount = 4000,
                Description = "Text embedding Ada 002",
                Name = "text-embedding-ada-002",
                QuotasPrice = 5,
                DimensionTypeId = 1536
            };
            
            var textEmbedding3Small = new EmbeddingModel()
            {
                Id = EmbeddingModelList.text_embedding_3_small,
                MaxInputCount = 4000,
                Description = "Text embedding 3 small",
                Name = "text-embedding-ada-002",
                QuotasPrice = 1,
                DimensionTypeId = 1536
            };

            var textEmbedding3Large = new EmbeddingModel()
            {
                Id = EmbeddingModelList.text_embedding_3_large,
                MaxInputCount = 4000,
                Description = "Text embedding 3 large",
                Name = "text_embedding_3_large",
                QuotasPrice = 8,
                DimensionTypeId = 3072
            };

            builder.HasData(textEmbeddingAda002, textEmbedding3Large, textEmbedding3Small);
        }
    }
}

using Domain.Entities.Embeddings;
using Domain.Entities.Youtube;
using Domain.Interfaces;
using Domain.Interfaces.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Embeddings
{
    public class EmbeddingsService : IEmbeddingsService
    {
        private readonly IRepository<EmbeddingValue> value_repository;
        private readonly IRepository<EmbeddingDimensionValue> Dimension_repository;
        private readonly IRepository<EmbeddingData> data_repository;

        public EmbeddingsService(IRepository<EmbeddingValue> value_repository,
                                 IRepository<EmbeddingDimensionValue> Dimension_repository,
                                 IRepository<EmbeddingData> data_repository)
        {
            this.value_repository = value_repository;
            this.Dimension_repository = Dimension_repository;
            this.data_repository = data_repository;
        }

        public async Task AddEmbeddingToComment(double[] embedding, int DimensionCount, Comment comment)
        {
            EmbeddingData embeddingData;
            if (comment.EmbeddingData == null)
            {
                var newEmbeddingData = new EmbeddingData()
                {
                    Comment = comment
                };
                comment.EmbeddingData = newEmbeddingData;

                await data_repository.AddAsync(newEmbeddingData);
                await data_repository.SaveChangesAsync();

                embeddingData = newEmbeddingData;
            }
            else
            {
                embeddingData = (await data_repository.GetAsync(e => e.Id == comment.EmbeddingData.Id, includeProperties: $"{nameof(EmbeddingData.Embeddings)}")).FirstOrDefault();
            }

            var DimensionValue = new EmbeddingDimensionValue()
            {
                DimensionTypeId = DimensionCount,
                EmbeddingData = embeddingData
            };

            embeddingData.Embeddings.Add(DimensionValue);

            await Dimension_repository.AddAsync(DimensionValue);

            foreach(var embeddingValue in embedding)
            {
                var value = new EmbeddingValue()
                {
                    EmbeddingDimensionValue = DimensionValue,
                    Value = embeddingValue
                };
                DimensionValue.Values.Add(value);

                await value_repository.AddAsync(value);
            }
        }
    }
}

using Domain.Entities.DimensionalityReduction;
using Domain.Entities.Embeddings;
using Domain.Entities.Youtube;
using Domain.Interfaces;
using Domain.Interfaces.Embeddings;
using Domain.Resources.Types;
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
        private readonly IRepository<EmbeddingDimensionValue> dimension_repository;
        private readonly IRepository<EmbeddingData> data_repository;
        private readonly IRepository<DimensionalityReductionValue> drValues_repository;

        public EmbeddingsService(IRepository<EmbeddingValue> value_repository,
                                 IRepository<EmbeddingDimensionValue> dimension_repository,
                                 IRepository<EmbeddingData> data_repository,
                                 IRepository<DimensionalityReductionValue> drValues_repository)
        {
            this.value_repository = value_repository;
            this.dimension_repository = dimension_repository;
            this.data_repository = data_repository;
            this.drValues_repository = drValues_repository;
        }

        public async Task AddEmbeddingToComment(double[] embedding, int dimensionCount, Comment comment)
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
                embeddingData = (await data_repository.GetAsync(e => e.Id == comment.EmbeddingData.Id,includeProperties:$"{nameof(EmbeddingData.DimensionalityReductionValue)}")).FirstOrDefault();
            }

            DimensionalityReductionValue DRv;
            if (embeddingData.DimensionalityReductionValue == null)
            {
                DRv = new DimensionalityReductionValue()
                {
                    EmbeddingDataId = embeddingData.Id,
                    TechniqueId = DimensionalityReductionTechniques.JSL
                };

                await drValues_repository.AddAsync(DRv);
            }
            else
            {
                DRv = embeddingData.DimensionalityReductionValue;
            }


            var dimensionValue = new EmbeddingDimensionValue()
            {
                DimensionTypeId = dimensionCount,
                DimensionalityReductionValue = DRv,
            };

            DRv.Embeddings.Add(dimensionValue);

            await dimension_repository.AddAsync(dimensionValue);
            foreach (var embeddingValue in embedding)
            {
                var value = new EmbeddingValue()
                {
                    EmbeddingDimensionValue = dimensionValue,
                    Value = embeddingValue
                };
                dimensionValue.Values.Add(value);

                await value_repository.AddAsync(value);
            }
        }
    }
}

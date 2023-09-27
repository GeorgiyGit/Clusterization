using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Embeddings
{
    public interface IEmbeddingsService
    {
        public Task AddEmbeddingToComment(double[] embedding, int DimensionCount, Comment comment);
    }
}

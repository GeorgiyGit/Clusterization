using Domain.DTOs.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Embeddings
{
    public interface ILoadEmbeddingsService
    {
        public Task LoadEmbeddingsByWorkspace(int workspaceId);
    }
}

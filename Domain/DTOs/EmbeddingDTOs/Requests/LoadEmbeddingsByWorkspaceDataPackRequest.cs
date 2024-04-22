using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.EmbeddingDTOs.Requests
{
    public class LoadEmbeddingsByWorkspaceDataPackRequest
    {
        public int PackId { get; set; }
        public string EmbeddingModelId { get; set; }
    }
}

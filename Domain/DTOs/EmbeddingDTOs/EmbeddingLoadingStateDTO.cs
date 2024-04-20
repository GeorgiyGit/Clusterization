using Domain.DTOs.EmbeddingModelDTOs.Responses;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.EmbeddingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.EmbeddingDTOs
{
    public class EmbeddingLoadingStateDTO
    {
        public int Id { get; set; }

        public bool IsAllEmbeddingsLoaded { get; set; }

        public EmbeddingModelDTO EmbeddingModel { get; set; }

        public int? ProfileId { get; set; }

        public int? AddPackId { get; set; }
    }
}

using Accord.Collections;
using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ExternalData
{
    public class ExternalObject
    {
        public string FullId { get; set; }
        public string Id { get; set; }
        public string Session { get; set; }
        public string Text { get; set; }

        public ICollection<ClusterizationWorkspace> Workspaces { get; set; } = new HashSet<ClusterizationWorkspace>();
        public ICollection<ClusterizationEntity> ClusterizationEntities { get; set; } = new HashSet<ClusterizationEntity>();

        public EmbeddingData? EmbeddingData { get; set; }
        public int? EmbeddingDataId { get; set; }
    }
}

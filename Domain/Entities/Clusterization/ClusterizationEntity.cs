﻿using Domain.Entities.Embeddings;
using Domain.Entities.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationEntity
    {
        public int Id { get; set; }

        public EmbeddingData? EmbeddingData { get; set; }
        public int? EmbeddingDataId { get; set; }

        public Comment Comment { get; set; }
        public string CommentId { get; set; }

        public DisplayedPoint? DisplayedPoint { get; set; }
        public int? DisplayedPointId { get; set; }

        public ICollection<Cluster> Clusters { get; set; } = new HashSet<Cluster>();

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }
    }
}

using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DimensionalityReduction
{
    public class DimensionalityReductionTechnique
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<DimensionalityReductionValue> Values { get; set; } = new HashSet<DimensionalityReductionValue>();
        public ICollection<ClusterizationProfile> Profiles { get; set; } = new HashSet<ClusterizationProfile>();
    }
}

using Domain.Entities.DimensionalityReduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clusterization
{
    public class ClusterizationWorkspaceDRTechnique
    {
        public int Id { get; set; }

        public DimensionalityReductionTechnique DRTechnique { get; set; }
        public string DRTechniqueId { get; set; }

        public ICollection<DimensionalityReductionValue> DRValues { get; set; } = new HashSet<DimensionalityReductionValue>();

        public ClusterizationWorkspace Workspace { get; set; }
        public int WorkspaceId { get; set; }
    }
}

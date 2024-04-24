using Domain.Resources.Types;
using Domain.Resources.Types.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs
{
    public class AddClusterizationWorkspaceRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TypeId { get; set; }

        public string VisibleType { get; set; } = VisibleTypes.AllCustomers;
        public string ChangingType { get; set; } = ChangingTypes.AllCustomers;
    }
}

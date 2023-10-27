using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    public interface IClusterizationDisplayedPointsService
    {
        public Task<ICollection<DisplayedPointDTO>> GetCommonWorkspaceDisplayedPoints(int workspaceId);
    }
}

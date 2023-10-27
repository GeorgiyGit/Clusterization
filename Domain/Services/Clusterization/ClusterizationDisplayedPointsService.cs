using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.Entities.Clusterization;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization
{
    public class ClusterizationDisplayedPointsService:IClusterizationDisplayedPointsService
    {
        private readonly IRepository<ClusterizationEntity> entities_repository;

        public ClusterizationDisplayedPointsService(IRepository<ClusterizationEntity> entities_repository)
        {
            this.entities_repository = entities_repository;
        }

        public Task<ICollection<DisplayedPointDTO>> GetCommonWorkspaceDisplayedPoints(int workspaceId)
        {
            throw new NotImplementedException();
        }
    }
}

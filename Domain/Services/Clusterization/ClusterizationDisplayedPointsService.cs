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
    public class ClusterizationDisplayedPointsService : IClusterizationDisplayedPointsService
    {
        private readonly IRepository<DisplayedPoint> points_repository;

        public ClusterizationDisplayedPointsService(IRepository<DisplayedPoint> points_repository)
        {
            this.points_repository = points_repository;
        }

        public async Task<DisplayedPointValueDTO> GetDisplayedPointTextValue(int pointId)
        {
            var point = (await points_repository.FindAsync(pointId));

            return new DisplayedPointValueDTO()
            {
                Value = point.Value
            };
        }
    }
}

using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.Entities.Clusterization.Displaying;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Interfaces.Other;

namespace Domain.Services.Clusterization.Displaying
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
            var point = (await points_repository.GetAsync(e => e.Id == pointId, includeProperties: $"{nameof(DisplayedPoint.DataObject)}")).FirstOrDefault();

            if (point == null) return null;

            return new DisplayedPointValueDTO()
            {
                Value = point.DataObject.Text
            };
        }
    }
}

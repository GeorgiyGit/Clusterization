using Domain.DTOs.ClusterizationDTOs.DisplayedPointDTOs;
using Domain.Entities.Clusterization.Displaying;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Interfaces.Other;

namespace Domain.Services.Clusterization.Displaying
{
    public class ClusterizationDisplayedPointsService : IClusterizationDisplayedPointsService
    {
        private readonly IRepository<DisplayedPoint> _pointsRepository;

        public ClusterizationDisplayedPointsService(IRepository<DisplayedPoint> points_repository)
        {
            _pointsRepository = points_repository;
        }

        public async Task<DisplayedPointValueDTO> GetDisplayedPointTextValue(int pointId)
        {
            var point = (await _pointsRepository.GetAsync(e => e.Id == pointId, includeProperties: $"{nameof(DisplayedPoint.DataObject)}")).FirstOrDefault();

            if (point == null) return null;

            return new DisplayedPointValueDTO()
            {
                Value = point.DataObject.Text
            };
        }
    }
}

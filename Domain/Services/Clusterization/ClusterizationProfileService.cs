using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
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
    public class ClusterizationProfileService : IClusterizationProfileService
    {
        private readonly IRepository<ClusterizationProfile> repository;
        public Task AddProfile(AddClusterizationProfileDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ClusterizationProfileDTO> GetProfileById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<SimpleClusterizationProfileDTO>> GetProfiles(GetClusterizationProfilesRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfile(UpdateClusterizationProfileDTO model)
        {
            throw new NotImplementedException();
        }
    }
}

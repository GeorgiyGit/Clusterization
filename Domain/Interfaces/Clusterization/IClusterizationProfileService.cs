using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization
{
    internal interface IClusterizationProfileService
    {
        public Task AddProfile(AddClusterizationProfileDTO model);
        public Task UpdateProfile(UpdateClusterizationProfileDTO model);

        public Task<ICollection<SimpleClusterizationProfileDTO>> GetProfiles(GetClusterizationProfilesRequestDTO request);

        public Task<ClusterizationProfileDTO> GetProfileById(int id);
    }
}

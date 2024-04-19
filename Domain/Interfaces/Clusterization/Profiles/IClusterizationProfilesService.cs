﻿using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Profiles
{
    public interface IClusterizationProfilesService
    {
        public Task Add(AddClusterizationProfileRequest model);
        
        public Task<ClusterizationProfileDTO> GetFullById(int id);
        public Task<SimpleClusterizationProfileDTO> GetSimpleById(int id);

        public Task<ICollection<SimpleClusterizationProfileDTO>> GetCollection(GetClusterizationProfilesRequest request);
        public Task<ICollection<SimpleClusterizationProfileDTO>> GetCustomerCollection(CustomerGetClusterizationProfilesRequest request);
        
        public Task Elect(int id);
        public Task UnElect(int id);
    }
}

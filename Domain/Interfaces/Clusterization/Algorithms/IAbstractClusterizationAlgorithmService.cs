﻿using Domain.DTOs;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Clusterization.Algorithms
{
    public interface IAbstractClusterizationAlgorithmService<AddDTO,GetDTO>
    {
        public Task AddAlgorithm(AddDTO model);
        public Task<ICollection<GetDTO>> GetAllAlgorithms();
        public Task<ICollection<GetDTO>> GetAlgorithms(PageParameters pageParameters);
        public Task<int> CalculateQuotasCount(int dataObjectsCount, int dimensionCount);

        public Task ClusterData(int profileId);
        public Task ClusterDataBackgroundJob(int profileId, string groupTaskId, string userId, List<string> subTaskIds);

        public Task<bool> ReviewWorkspaceIsProfilesInCalculation(int workspaceId);
    }
}

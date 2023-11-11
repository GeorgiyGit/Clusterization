using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Entities.Clusterization;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Services.Clusterization.Profiles
{
    public class ClusterizationProfilesService : IClusterizationProfilesService
    {
        private readonly IRepository<ClusterizationProfile> repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        private readonly IGeneralClusterizationAlgorithmService generalAlgorithmService;
        public ClusterizationProfilesService(IRepository<ClusterizationProfile> repository,
                                             IStringLocalizer<ErrorMessages> localizer,
                                             IMapper mapper,
                                             IGeneralClusterizationAlgorithmService generalAlgorithmService)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.generalAlgorithmService = generalAlgorithmService;
        }

        public async Task Add(AddClusterizationProfileDTO model)
        {
            var oldProfile = (await repository.GetAsync(c=>c.WorkspaceId== model.WorkspaceId && c.AlgorithmId==model.AlgorithmId && c.DimensionCount == model.DimensionCount && c.DimensionalityReductionTechniqueId==model.DRTechniqueId)).FirstOrDefault();

            if (oldProfile != null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileAlreadyExist], System.Net.HttpStatusCode.BadGateway);

            var newProfile = new ClusterizationProfile()
            {
                WorkspaceId = model.WorkspaceId,
                AlgorithmId = model.AlgorithmId,
                DimensionCount = model.DimensionCount,
                DimensionalityReductionTechniqueId = model.DRTechniqueId
            };

            await repository.AddAsync(newProfile);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<SimpleClusterizationProfileDTO>> GetCollection(GetClusterizationProfilesRequestDTO request)
        {
            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.WorkspaceId == request.WorkspaceId;

            if (request.AlgorithmTypeId != null) filterCondition = filterCondition.And(e => e.Algorithm.TypeId == request.AlgorithmTypeId);
            if (request.DimensionCount != null) filterCondition = filterCondition.And(e => e.DimensionCount == request.DimensionCount);

            var profiles = (await repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).ToList();

            var mappedProfiles = mapper.Map<ICollection<SimpleClusterizationProfileDTO>>(profiles).ToList();
            for (int i = 0; i < profiles.Count(); i++)
            {
                var type = await generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profiles[i].AlgorithmId);
                mappedProfiles[i].AlgorithmType = type;
            }

            return mappedProfiles;
        }

        public async Task<ClusterizationProfileDTO> GetFullById(int id)
        {
            var profile = (await repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedProfile = mapper.Map<ClusterizationProfileDTO>(profile);

            var type = await generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profile.AlgorithmId);
            mappedProfile.AlgorithmType = type;

            return mappedProfile;
        }

        public async Task<SimpleClusterizationProfileDTO> GetSimpleById(int id)
        {
            var profile = (await repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedProfile = mapper.Map<SimpleClusterizationProfileDTO>(profile);

            var type = await generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profile.AlgorithmId);
            mappedProfile.AlgorithmType = type;

            return mappedProfile;
        }
    }
}

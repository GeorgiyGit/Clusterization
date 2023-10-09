using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Entities.Clusterization;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Profiles
{
    public class ClusterizationProfilesService : IClusterizationProfilesService
    {
        private readonly IRepository<ClusterizationProfile> repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;

        public ClusterizationProfilesService(IRepository<ClusterizationProfile> repository,
                                             IStringLocalizer<ErrorMessages> localizer,
                                             IMapper mapper)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
        }

        public async Task Add(AddClusterizationProfileDTO model)
        {
            var oldProfile = (await repository.GetAsync(c=>c.WorkspaceId== model.WorkspaceId && c.AlgorithmId==model.AlgorithmId && c.DimensionTypeId==model.DimensionTypeId)).FirstOrDefault();

            if (oldProfile != null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileAlreadyExist], System.Net.HttpStatusCode.BadGateway);

            var newProfile = new ClusterizationProfile()
            {
                WorkspaceId = model.WorkspaceId,
                AlgorithmId = model.AlgorithmId,
                DimensionTypeId = model.DimensionTypeId
            };

            await repository.AddAsync(newProfile);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<SimpleClusterizationProfileDTO>> GetCollection(GetClusterizationProfilesRequestDTO request)
        {
            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.WorkspaceId == request.WorkspaceId;

            if (request.AlgorithmTypeId != null) filterCondition.And(e => e.Algorithm.TypeId == request.AlgorithmTypeId);
            if (request.DimensionTypeId != null) filterCondition.And(e => e.DimensionTypeId == request.DimensionTypeId);

            var profiles = (await repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.Algorithm.Type)}")).ToList();

            return mapper.Map<ICollection<SimpleClusterizationProfileDTO>>(profiles);
        }

        public async Task<ClusterizationProfileDTO> GetFullById(int id)
        {
            var profile = (await repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Algorithm.Type)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.Workspace)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            return mapper.Map<ClusterizationProfileDTO>(profile);
        }
    }
}

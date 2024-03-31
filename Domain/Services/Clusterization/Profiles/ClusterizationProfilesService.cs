using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;

namespace Domain.Services.Clusterization.Profiles
{
    public class ClusterizationProfilesService : IClusterizationProfilesService
    {
        private readonly IRepository<ClusterizationProfile> repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        private readonly IGeneralClusterizationAlgorithmService generalAlgorithmService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public ClusterizationProfilesService(IRepository<ClusterizationProfile> repository,
                                             IStringLocalizer<ErrorMessages> localizer,
                                             IMapper mapper,
                                             IGeneralClusterizationAlgorithmService generalAlgorithmService,
                                             IUserService userService,
                                             IQuotasControllerService quotasControllerService)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.generalAlgorithmService = generalAlgorithmService;
            _userService = userService;
            _quotasControllerService = quotasControllerService;
        }

        public async Task Add(AddClusterizationProfileDTO model)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.WorkspaceId == model.WorkspaceId && e.AlgorithmId == model.AlgorithmId && e.DimensionCount == model.DimensionCount && e.DimensionalityReductionTechniqueId == model.DRTechniqueId && e.ChangingType == model.ChangingType;

            if (model.VisibleType == VisibleTypes.AllCustomers)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.OwnerId == userId && e.VisibleType == VisibleTypes.AllCustomers);
            }

            var oldProfile = (await repository.GetAsync(filter: filterCondition)).FirstOrDefault();

            if (oldProfile != null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileAlreadyExist], System.Net.HttpStatusCode.BadGateway);


            string type = null;

            if (model.VisibleType == VisibleTypes.AllCustomers)
            {
                type = QuotasTypes.PublicProfiles;
            }
            else if (model.VisibleType == VisibleTypes.OnlyOwner)
            {
                type = QuotasTypes.PrivateProfiles;
            }

            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, type, 1,Guid.NewGuid().ToString());

            if (!quotasResult)
            {
                throw new HttpException(localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }


            var newProfile = new ClusterizationProfile()
            {
                WorkspaceId = model.WorkspaceId,
                AlgorithmId = model.AlgorithmId,
                DimensionCount = model.DimensionCount,
                DimensionalityReductionTechniqueId = model.DRTechniqueId,
                VisibleType = model.VisibleType,
                ChangingType = model.ChangingType,
                OwnerId = userId
            };

            await repository.AddAsync(newProfile);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<SimpleClusterizationProfileDTO>> GetCollection(GetClusterizationProfilesRequestDTO request)
        {
            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.WorkspaceId == request.WorkspaceId;

            if (request.AlgorithmTypeId != null) filterCondition = filterCondition.And(e => e.Algorithm.TypeId == request.AlgorithmTypeId);
            if (request.DimensionCount != null) filterCondition = filterCondition.And(e => e.DimensionCount == request.DimensionCount);

            var userId = await _userService.GetCurrentUserId();
            if (userId != null)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers || e.OwnerId == userId);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }

            var pageParameters = request.PageParameters;
            var profiles = (await repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Owner)}"))
                                            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                            .Take(pageParameters.PageSize).ToList();

            var mappedProfiles = mapper.Map<ICollection<SimpleClusterizationProfileDTO>>(profiles).ToList();
            for (int i = 0; i < profiles.Count(); i++)
            {
                var type = await generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profiles[i].AlgorithmId);
                mappedProfiles[i].AlgorithmType = type;

                string fullAlgName = "";
                if (type.Id == ClusterizationAlgorithmTypes.OneCluster)
                {
                    var algorithm = profiles[i].Algorithm as OneClusterAlgorithm;
                    fullAlgName = algorithm.ClusterColor + "";
                }
                else if (type.Id == ClusterizationAlgorithmTypes.KMeans)
                {
                    var algorithm = profiles[i].Algorithm as KMeansAlgorithm;
                    fullAlgName = algorithm.NumClusters + "";
                }
                else if (type.Id == ClusterizationAlgorithmTypes.DBScan)
                {
                    var algorithm = profiles[i].Algorithm as DBScanAlgorithm;
                    fullAlgName = algorithm.Epsilon + " " + algorithm.MinimumPointsPerCluster;
                }
                else if (type.Id == ClusterizationAlgorithmTypes.SpectralClustering)
                {
                    var algorithm = profiles[i].Algorithm as SpectralClusteringAlgorithm;
                    fullAlgName = algorithm.NumClusters + " " + algorithm.Gamma;
                }
                else if (type.Id == ClusterizationAlgorithmTypes.GaussianMixture)
                {
                    var algorithm = profiles[i].Algorithm as GaussianMixtureAlgorithm;
                    fullAlgName = algorithm.NumberOfComponents + "";
                }

                mappedProfiles[i].FullTitle = type.Name + " (" + fullAlgName + ") " + mappedProfiles[i].DimensionalityReductionTechnique.Id + " " + mappedProfiles[i].DimensionCount;

            }

            return mappedProfiles;
        }

        public async Task<ClusterizationProfileDTO> GetFullById(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            var profile = (await repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.Owner)}")).FirstOrDefault();

            if (profile == null || (profile.VisibleType == VisibleTypes.OnlyOwner && profile.OwnerId != userId)) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedProfile = mapper.Map<ClusterizationProfileDTO>(profile);

            var type = await generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profile.AlgorithmId);
            mappedProfile.AlgorithmType = type;

            return mappedProfile;
        }

        public async Task<SimpleClusterizationProfileDTO> GetSimpleById(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            var profile = (await repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).FirstOrDefault();

            if (profile == null || (profile.VisibleType == VisibleTypes.OnlyOwner && profile.OwnerId != userId)) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedProfile = mapper.Map<SimpleClusterizationProfileDTO>(profile);

            var type = await generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profile.AlgorithmId);
            mappedProfile.AlgorithmType = type;

            return mappedProfile;
        }

        public async Task Elect(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var profile = (await repository.GetAsync(e => e.Id == id && e.OwnerId == userId, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            if (!profile.IsElected)
            {
                profile.IsElected = true;

                await repository.SaveChangesAsync();
            }
        }
        public async Task UnElect(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var profile = (await repository.GetAsync(e => e.Id == id && e.OwnerId==userId, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DimensionalityReductionTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            if (profile.IsElected)
            {
                profile.IsElected = false;

                await repository.SaveChangesAsync();
            }
        }
    }
}

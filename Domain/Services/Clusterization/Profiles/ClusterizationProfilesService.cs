using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.ProfileDTOs.RequestDTOs;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using Domain.Entitie.Clusterization.Algorithms.Non_hierarchical;
using Domain.Interfaces.Other;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Embeddings;
using Domain.Interfaces.Embeddings;
using Domain.Resources.Types.Clusterization;
using Domain.Entities.Clusterization.Workspaces;

namespace Domain.Services.Clusterization.Profiles
{
    public class ClusterizationProfilesService : IClusterizationProfilesService
    {
        private readonly IRepository<ClusterizationProfile> _repository;
        private readonly IRepository<EmbeddingLoadingState> _embeddingLoadingStatesRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IGeneralClusterizationAlgorithmService _generalAlgorithmService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;
        public ClusterizationProfilesService(IRepository<ClusterizationProfile> repository,
                                             IStringLocalizer<ErrorMessages> localizer,
                                             IRepository<EmbeddingLoadingState> embeddingLoadingStatesRepository,
                                             IRepository<ClusterizationWorkspace> workspacesRepositor,
                                             IMapper mapper,
                                             IGeneralClusterizationAlgorithmService generalAlgorithmService,
                                             IUserService userService,
                                             IQuotasControllerService quotasControllerService,
                                             IEmbeddingLoadingStatesService embeddingLoadingStatesService)
        {
            _repository = repository;
            _workspacesRepository = workspacesRepositor;
            _localizer = localizer;
            _mapper = mapper;
            _generalAlgorithmService = generalAlgorithmService;
            _userService = userService;
            _quotasControllerService = quotasControllerService;
            _embeddingLoadingStatesRepository = embeddingLoadingStatesRepository;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
        }

        public async Task Add(AddClusterizationProfileRequest model)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.WorkspaceId == model.WorkspaceId && e.AlgorithmId == model.AlgorithmId && e.DimensionCount == model.DimensionCount && e.DRTechniqueId == model.DRTechniqueId && e.ChangingType == model.ChangingType && e.EmbeddingModelId == model.EmbeddingModelId;

            if (model.VisibleType == VisibleTypes.AllCustomers)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.OwnerId == userId && e.VisibleType == VisibleTypes.AllCustomers);
            }

            var oldProfile = (await _repository.GetAsync(filter: filterCondition)).FirstOrDefault();

            if (oldProfile != null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileAlreadyExist], System.Net.HttpStatusCode.BadGateway);


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
                throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }


            var newProfile = new ClusterizationProfile()
            {
                WorkspaceId = model.WorkspaceId,
                AlgorithmId = model.AlgorithmId,
                DimensionCount = model.DimensionCount,
                DRTechniqueId = model.DRTechniqueId,
                EmbeddingModelId = model.EmbeddingModelId,
                VisibleType = model.VisibleType,
                ChangingType = model.ChangingType,
                OwnerId = userId
            };

            var profileState = new EmbeddingLoadingState()
            {
                Profile = newProfile,
                EmbeddingModelId = model.EmbeddingModelId,
                IsAllEmbeddingsLoaded = false
            };

            await _embeddingLoadingStatesRepository.AddAsync(profileState);

            await _repository.AddAsync(newProfile);
            await _repository.SaveChangesAsync();

            await _embeddingLoadingStatesService.ReviewStates(model.WorkspaceId);
        }

        public async Task<ICollection<SimpleClusterizationProfileDTO>> GetCollection(GetClusterizationProfilesRequest request)
        {
            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.WorkspaceId == request.WorkspaceId;

            if (request.AlgorithmTypeId != null) filterCondition = filterCondition.And(e => e.Algorithm.TypeId == request.AlgorithmTypeId);
            if (request.DimensionCount != null) filterCondition = filterCondition.And(e => e.DimensionCount == request.DimensionCount);
            if (request.EmbeddingModelId != null) filterCondition = filterCondition.And(e => e.EmbeddingModelId == request.EmbeddingModelId);

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
            var profiles = (await _repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Owner)},{nameof(ClusterizationProfile.EmbeddingModel)}"))
                                            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                            .Take(pageParameters.PageSize).ToList();

            var mappedProfiles = _mapper.Map<ICollection<SimpleClusterizationProfileDTO>>(profiles).ToList();
            for (int i = 0; i < profiles.Count(); i++)
            {
                var type = await _generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profiles[i].AlgorithmId);
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
                else if (type.Id == ClusterizationAlgorithmTypes.DBSCAN)
                {
                    var algorithm = profiles[i].Algorithm as DBSCANAlgorithm;
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

                mappedProfiles[i].FullTitle = type.Name + " (" + fullAlgName + ") " + mappedProfiles[i].DRTechnique.Id + " " + mappedProfiles[i].DimensionCount + " " + mappedProfiles[i].EmbeddingModel.Name;

            }

            return mappedProfiles;
        }
        public async Task<ICollection<SimpleClusterizationProfileDTO>> GetCustomerCollection(CustomerGetClusterizationProfilesRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            Expression<Func<ClusterizationProfile, bool>> filterCondition = e => e.OwnerId == userId;

            if (request.AlgorithmTypeId != null) filterCondition = filterCondition.And(e => e.Algorithm.TypeId == request.AlgorithmTypeId);
            if (request.DimensionCount != null) filterCondition = filterCondition.And(e => e.DimensionCount == request.DimensionCount);
            if (request.EmbeddingModelId != null) filterCondition = filterCondition.And(e => e.EmbeddingModelId == request.EmbeddingModelId);

            if (userId != null)
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers || e.OwnerId == userId);
            }
            else
            {
                filterCondition = filterCondition.And(e => e.VisibleType == VisibleTypes.AllCustomers);
            }

            var pageParameters = request.PageParameters;
            var profiles = (await _repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Owner)},{nameof(ClusterizationProfile.EmbeddingModel)}"))
                                            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                            .Take(pageParameters.PageSize).ToList();

            var mappedProfiles = _mapper.Map<ICollection<SimpleClusterizationProfileDTO>>(profiles).ToList();
            for (int i = 0; i < profiles.Count(); i++)
            {
                var type = await _generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profiles[i].AlgorithmId);
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
                else if (type.Id == ClusterizationAlgorithmTypes.DBSCAN)
                {
                    var algorithm = profiles[i].Algorithm as DBSCANAlgorithm;
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

                mappedProfiles[i].FullTitle = type.Name + " (" + fullAlgName + ") " + mappedProfiles[i].DRTechnique.Id + " " + mappedProfiles[i].DimensionCount;

            }

            return mappedProfiles;
        }
        public async Task<ClusterizationProfileDTO> GetFullById(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            var profile = (await _repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.Clusters)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.Workspace)},{nameof(ClusterizationProfile.Owner)},{nameof(ClusterizationProfile.EmbeddingModel)},{nameof(ClusterizationProfile.EmbeddingLoadingState)}")).FirstOrDefault();

            if (profile == null || (profile.VisibleType == VisibleTypes.OnlyOwner && profile.OwnerId != userId)) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedProfile = _mapper.Map<ClusterizationProfileDTO>(profile);

            var type = await _generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profile.AlgorithmId);
            mappedProfile.AlgorithmType = type;

            return mappedProfile;
        }

        public async Task<SimpleClusterizationProfileDTO> GetSimpleById(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            var profile = (await _repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.Algorithm)},{nameof(ClusterizationProfile.EmbeddingModel)}")).FirstOrDefault();
            if (profile == null || (profile.VisibleType == VisibleTypes.OnlyOwner && profile.OwnerId != userId)) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedProfile = _mapper.Map<SimpleClusterizationProfileDTO>(profile);

            var type = await _generalAlgorithmService.GetAlgorithmTypeByAlgorithmId(profile.AlgorithmId);
            mappedProfile.AlgorithmType = type;

            return mappedProfile;
        }

        public async Task<int> CalculateQuotasCount(int profileId)
        {
            var profile = (await _repository.GetAsync(e => e.Id == profileId, includeProperties:$"{nameof(ClusterizationProfile.EmbeddingModel)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);
            if (profile.VisibleType == VisibleTypes.OnlyOwner)
            {
                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (profile.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceVisibleTypeIsOnlyOwner], HttpStatusCode.BadRequest);
            }

            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == profile.WorkspaceId)).FirstOrDefault();
            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            return workspace.EntitiesCount * profile.EmbeddingModel.QuotasPrice;
        }

        public async Task Elect(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var profile = (await _repository.GetAsync(e => e.Id == id && e.OwnerId == userId, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            if (!profile.IsElected)
            {
                profile.IsElected = true;

                await _repository.SaveChangesAsync();
            }
        }
        public async Task UnElect(int id)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var profile = (await _repository.GetAsync(e => e.Id == id && e.OwnerId==userId, includeProperties: $"{nameof(ClusterizationProfile.DimensionType)},{nameof(ClusterizationProfile.DRTechnique)},{nameof(ClusterizationProfile.Algorithm)}")).FirstOrDefault();

            if (profile == null) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            if (profile.IsElected)
            {
                profile.IsElected = false;

                await _repository.SaveChangesAsync();
            }
        }
    }
}

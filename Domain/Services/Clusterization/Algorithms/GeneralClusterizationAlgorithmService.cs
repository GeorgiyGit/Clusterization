using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Exceptions;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using Domain.Interfaces.Other;
using Domain.Resources.Types.Clusterization;
using System;
using System.Net;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Interfaces.Customers;
using Domain.Entities.Clusterization.Profiles;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Requests;

namespace Domain.Services.Clusterization.Algorithms
{
    public class GeneralClusterizationAlgorithmService : IGeneralClusterizationAlgorithmService
    {
        private readonly IRepository<ClusterizationAbstactAlgorithm> _abstractAlgorithmsRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<ClusterizationProfile> _profilesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmRequest, KMeansAlgorithmDTO> _kMeansService;
        private readonly IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmRequest, OneClusterAlgorithmDTO> _oneClusterService;
        private readonly IAbstractClusterizationAlgorithmService<AddDBSCANAlgorithmRequest, DBSCANAlgorithmDTO> _dbSCANService;
        private readonly IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO> _spectralClusteringService;
        private readonly IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmRequest, GaussianMixtureAlgorithmDTO> _gaussianMixtureService;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public GeneralClusterizationAlgorithmService(IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmRequest, KMeansAlgorithmDTO> kMeansService,
                                                     IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmRequest, OneClusterAlgorithmDTO> oneClusterService,
                                                     IAbstractClusterizationAlgorithmService<AddDBSCANAlgorithmRequest, DBSCANAlgorithmDTO> dbSCANService,
                                                     IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO> spectralClusteringService,
                                                     IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmRequest, GaussianMixtureAlgorithmDTO> gaussianMixtureService,
                                                     IStringLocalizer<ErrorMessages> localizer,
                                                     IRepository<ClusterizationAbstactAlgorithm> abstractRepository,
                                                     IRepository<ClusterizationWorkspace> workspacesRepository,
                                                     IRepository<ClusterizationProfile> profilesRepository,
                                                     IMapper mapper,
                                                     IUserService userService)
        {
            _kMeansService = kMeansService;
            _oneClusterService = oneClusterService;
            _dbSCANService = dbSCANService;
            _spectralClusteringService = spectralClusteringService;
            _gaussianMixtureService = gaussianMixtureService;

            _localizer = localizer;
            _abstractAlgorithmsRepository = abstractRepository;
            _mapper = mapper;
            _userService = userService;

            _workspacesRepository = workspacesRepository;
            _profilesRepository = profilesRepository;
        }

        public async Task<ICollection<AbstractAlgorithmDTO>> GetAllAlgorithms(string typeId)
        {
            if (typeId == ClusterizationAlgorithmTypes.KMeans)
            {
                return (await _kMeansService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.OneCluster)
            {
                return (await _oneClusterService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.DBSCAN)
            {
                return (await _dbSCANService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.SpectralClustering)
            {
                return (await _spectralClusteringService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.GaussianMixture)
            {
                return (await _gaussianMixtureService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist], System.Net.HttpStatusCode.NotFound);
            }
        }
        public async Task<ICollection<AbstractAlgorithmDTO>> GetAlgorithms(GetAlgorithmsRequest request)
        {
            if (request.TypeId == ClusterizationAlgorithmTypes.KMeans)
            {
                return (await _kMeansService.GetAlgorithms(request.PageParameters)).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (request.TypeId == ClusterizationAlgorithmTypes.OneCluster)
            {
                return (await _oneClusterService.GetAlgorithms(request.PageParameters)).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (request.TypeId == ClusterizationAlgorithmTypes.DBSCAN)
            {
                return (await _dbSCANService.GetAlgorithms(request.PageParameters)).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (request.TypeId == ClusterizationAlgorithmTypes.SpectralClustering)
            {
                return (await _spectralClusteringService.GetAlgorithms(request.PageParameters)).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (request.TypeId == ClusterizationAlgorithmTypes.GaussianMixture)
            {
                return (await _gaussianMixtureService.GetAlgorithms(request.PageParameters)).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist], System.Net.HttpStatusCode.NotFound);
            }
        }

        public async Task<SimpleAlgorithmTypeDTO?> GetAlgorithmTypeByAlgorithmId(int algorithmId)
        {
            var algorithm = (await _abstractAlgorithmsRepository.GetAsync(c => c.Id == algorithmId, includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}")).FirstOrDefault();

            if (algorithm == null) return null;

            return _mapper.Map<SimpleAlgorithmTypeDTO>(algorithm.Type);
        }

        public async Task<int> CalculateQuotasCountByWorkspace(string algorithmTypeId, int workspaceId, int dimensionCount)
        {
            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == workspaceId)).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            if (workspace.ChangingType == ChangingTypes.OnlyOwner)
            {
                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (workspace.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceVisibleTypeIsOnlyOwner], HttpStatusCode.BadRequest);
            }

            return await CalculateQuotasCount(algorithmTypeId, workspace.EntitiesCount, dimensionCount);
        }
        public async Task<int> CalculateQuotasCount(string algorithmTypeId, int dataObjectsCount, int dimensionCount)
        {
            if (algorithmTypeId == ClusterizationAlgorithmTypes.KMeans)
            {
                return await _kMeansService.CalculateQuotasCount(dataObjectsCount, dimensionCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.OneCluster)
            {
                return await _oneClusterService.CalculateQuotasCount(dataObjectsCount, dimensionCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.DBSCAN)
            {
                return await _dbSCANService.CalculateQuotasCount(dataObjectsCount, dimensionCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.SpectralClustering)
            {
                return await _spectralClusteringService.CalculateQuotasCount(dataObjectsCount, dimensionCount);
            }
            else if (algorithmTypeId == ClusterizationAlgorithmTypes.GaussianMixture)
            {
                return await _gaussianMixtureService.CalculateQuotasCount(dataObjectsCount, dimensionCount);
            }
            else
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist], System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}

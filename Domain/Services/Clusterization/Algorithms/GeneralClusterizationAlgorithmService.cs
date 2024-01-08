﻿using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.TypeDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Algorithms
{
    public class GeneralClusterizationAlgorithmService : IGeneralClusterizationAlgorithmService
    {
        private readonly IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO, KMeansAlgorithmDTO> kMeansService;
        private readonly IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmDTO, OneClusterAlgorithmDTO> oneClusterService;
        private readonly IAbstractClusterizationAlgorithmService<AddDBScanAlgorithmDTO, DBScanAlgorithmDTO> dbSCANService;
        private readonly IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmDTO, SpectralClusteringAlgorithmDTO> spectralClusteringService;
        private readonly IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmDTO, GaussianMixtureAlgorithmDTO> gaussianMixtureService;

        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IRepository<ClusterizationAbstactAlgorithm> abstractRepository;
        private readonly IMapper mapper;
        public GeneralClusterizationAlgorithmService(IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO, KMeansAlgorithmDTO> kMeansService,
                                                     IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmDTO, OneClusterAlgorithmDTO> oneClusterService,
                                                     IAbstractClusterizationAlgorithmService<AddDBScanAlgorithmDTO, DBScanAlgorithmDTO> dbSCANService,
                                                     IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmDTO, SpectralClusteringAlgorithmDTO> spectralClusteringService,
                                                     IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmDTO, GaussianMixtureAlgorithmDTO> gaussianMixtureService,
                                                     IStringLocalizer<ErrorMessages> localizer,
                                                     IRepository<ClusterizationAbstactAlgorithm> abstractRepository,
                                                     IMapper mapper)
        {
            this.kMeansService = kMeansService;
            this.oneClusterService = oneClusterService;
            this.dbSCANService = dbSCANService;
            this.spectralClusteringService = spectralClusteringService;
            this.gaussianMixtureService = gaussianMixtureService;

            this.localizer = localizer;
            this.abstractRepository = abstractRepository;
            this.mapper = mapper;
        }

        public async Task<ICollection<AbstractAlgorithmDTO>> GetAllAlgorithms(string typeId)
        {
            if (typeId == ClusterizationAlgorithmTypes.KMeans)
            {
                return (await kMeansService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.OneCluster)
            {
                return (await oneClusterService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.DBScan)
            {
                return (await dbSCANService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.SpectralClustering)
            {
                return (await spectralClusteringService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else if (typeId == ClusterizationAlgorithmTypes.GaussianMixture)
            {
                return (await gaussianMixtureService.GetAllAlgorithms()).Cast<AbstractAlgorithmDTO>().ToList();
            }
            else
            {
                throw new HttpException(localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist], System.Net.HttpStatusCode.NotFound);
            }
        }

        public async Task<SimpleAlgorithmTypeDTO?> GetAlgorithmTypeByAlgorithmId(int algorithmId)
        {
            var algorithm = (await abstractRepository.GetAsync(c => c.Id == algorithmId, includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}")).FirstOrDefault();

            if (algorithm == null) return null;

            return mapper.Map<SimpleAlgorithmTypeDTO>(algorithm.Type);
        }
    }
}

using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Exceptions;
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
        private readonly IStringLocalizer<ErrorMessages> localizer;
        public GeneralClusterizationAlgorithmService(IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO, KMeansAlgorithmDTO> kMeansService,
                                                     IStringLocalizer<ErrorMessages> localizer)
        {
            this.kMeansService = kMeansService;
            this.localizer = localizer;
        }

        public async Task<AbstractAlgorithmDTO> GetAllAlgorithms(string typeId)
        {
            if (typeId == ClusterizationAlgorithmTypes.KMeans)
            {
                return (AbstractAlgorithmDTO)await kMeansService.GetAllAlgorithms();
            }
            else
            {
                throw new HttpException(localizer[ErrorMessagePatterns.ClusterizationAlgorithmTypeIdNotExist], System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}

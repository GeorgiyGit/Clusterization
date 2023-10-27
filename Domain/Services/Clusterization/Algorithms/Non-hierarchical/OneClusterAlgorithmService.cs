using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class OneClusterAlgorithmService : IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmDTO, OneClusterAlgorithmDTO>
    {
        private readonly IRepository<OneClusterAlgorithm> repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        public OneClusterAlgorithmService(IRepository<OneClusterAlgorithm> repository,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMapper mapper)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
        }

        public async Task AddAlgorithm(AddOneClusterAlgorithmDTO model)
        {
            var list = await repository.GetAsync(c => c.ClusterColor == model.ClusterColor);

            if (list.Any()) throw new HttpException(localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new OneClusterAlgorithm()
            {
                ClusterColor = model.ClusterColor,
                TypeId = ClusterizationAlgorithmTypes.OneCluster
            };

            await repository.AddAsync(newAlg);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<OneClusterAlgorithmDTO>> GetAllAlgorithms()
        {
            var algorithms = await repository.GetAsync(includeProperties: $"{nameof(ClusterizationAbstactAlgorithm.Type)}");

            return mapper.Map<ICollection<OneClusterAlgorithmDTO>>(algorithms);
        }
    }
}

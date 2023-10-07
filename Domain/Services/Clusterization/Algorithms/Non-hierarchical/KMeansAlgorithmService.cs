using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Entities.Clusterization.Algorithms;
using Domain.Entities.Clusterization.Algorithms.Non_hierarchical;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Google.Apis.Util;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization.Algorithms.Non_hierarchical
{
    public class KMeansAlgorithmService : IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO, KMeansAlgorithmDTO>
    {
        private readonly IRepository<KMeansAlgorithm> repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        public KMeansAlgorithmService(IRepository<KMeansAlgorithm> repository,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMapper mapper)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
        }

        public async Task AddAlgorithm(AddKMeansAlgorithmDTO model)
        {
            var list = await repository.GetAsync(c => c.NumClusters == model.NumClusters && c.Seed == model.Seed);

            if (list.Any()) throw new HttpException(localizer[ErrorMessagePatterns.AlgorithmAlreadyExists], HttpStatusCode.BadRequest);

            var newAlg = new KMeansAlgorithm()
            {
                NumClusters = model.NumClusters,
                Seed = model.Seed,
                TypeId = ClusterizationAlgorithmTypes.KMeans
            };

            await repository.AddAsync(newAlg);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<KMeansAlgorithmDTO>> GetAllAlgorithms()
        {
            var types = await repository.GetAsync(includeProperties: $"{nameof(KMeansAlgorithm.Type)}");

            return mapper.Map<ICollection<KMeansAlgorithmDTO>>(types);
        }
    }
}

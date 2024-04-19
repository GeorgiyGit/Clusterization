using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings;
using Domain.Exceptions;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Embeddings
{
    public class EmbeddingLoadingStatesService:IEmbeddingLoadingStatesService
    {
        private readonly IRepository<EmbeddingLoadingState> _embeddingLoadingStates;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository;

        public EmbeddingLoadingStatesService(IRepository<EmbeddingLoadingState> embeddingLoadingStates,
            IRepository<EmbeddingModel> embeddingModelsRepository)
        {
            _embeddingLoadingStates = embeddingLoadingStates;
            _embeddingModelsRepository = embeddingModelsRepository;
        }

        public async Task AddStatesToPack(int packId)
        {
            var embeddingModels = await _embeddingModelsRepository.GetAllAsync();
        
            foreach(var embeddingModel in embeddingModels)
            {
                var newState = new EmbeddingLoadingState()
                {
                    EmbeddingModelId = embeddingModel.Id,
                    AddPackId = packId
                };

                await _embeddingLoadingStates.AddAsync(newState);
            }
            await _embeddingLoadingStates.SaveChangesAsync();
        }
    }
}

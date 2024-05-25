using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.EmbeddingModels;
using Domain.Entities.Embeddings;
using Domain.Interfaces.EmbeddingModels;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;

namespace Domain.Services.Embeddings
{
    public class EmbeddingLoadingStatesService:IEmbeddingLoadingStatesService
    {
        private readonly IRepository<EmbeddingLoadingState> _embeddingLoadingStates;
        private readonly IRepository<EmbeddingModel> _embeddingModelsRepository;
        private readonly IRepository<ClusterizationProfile> _profilesRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _addPacksRepository;

        private IEmbeddingModelsService _embeddingModelsService;
        public EmbeddingLoadingStatesService(IRepository<EmbeddingLoadingState> embeddingLoadingStates,
            IRepository<EmbeddingModel> embeddingModelsRepository,
            IRepository<ClusterizationProfile> profilesRepository,
            IRepository<WorkspaceDataObjectsAddPack> addPacksRepository,
            IEmbeddingModelsService embeddingModelsService)
        {
            _embeddingLoadingStates = embeddingLoadingStates;
            _embeddingModelsRepository = embeddingModelsRepository;
            _profilesRepository = profilesRepository;
            _addPacksRepository = addPacksRepository;

            _embeddingModelsService = embeddingModelsService;
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

        public async Task ReviewStates(int workspaceId)
        {
            var profiles = await _profilesRepository.GetAsync(e => e.WorkspaceId == workspaceId, includeProperties: $"{nameof(ClusterizationProfile.EmbeddingLoadingState)}");

            var packs = await _addPacksRepository.GetAsync(e => e.WorkspaceId == workspaceId && !e.IsDeleted, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.EmbeddingLoadingStates)}");

            Dictionary<string, bool> embeddingModelStates = new Dictionary<string, bool>();

            var models = await _embeddingModelsService.GetAll();

            foreach(var model in models)
            {
                var flag = false;
                foreach(var pack in packs)
                {
                    var state = pack.EmbeddingLoadingStates.Where(e => e.EmbeddingModelId == model.Id).FirstOrDefault();
                    if(state==null || !state.IsAllEmbeddingsLoaded)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag) embeddingModelStates.Add(model.Id, false);
                else embeddingModelStates.Add(model.Id, true);
            }

            foreach(var profile in profiles)
            {
                bool result = embeddingModelStates[profile.EmbeddingModelId];

                profile.EmbeddingLoadingState.IsAllEmbeddingsLoaded = result;
            }
            await _addPacksRepository.SaveChangesAsync();
        }
    }
}

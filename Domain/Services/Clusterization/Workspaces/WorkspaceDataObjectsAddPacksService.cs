using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Respones;
using Domain.DTOs.EmbeddingDTOs.Responses;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.Embeddings;
using Domain.Exceptions;
using Domain.Interfaces.Clusterization.Workspaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Resources.Types.Clusterization;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Domain.Services.Clusterization.Workspaces
{
    public class WorkspaceDataObjectsAddPacksService : IWorkspaceDataObjectsAddPacksService
    {
        private readonly IRepository<WorkspaceDataObjectsAddPack> _packsRepository;
        private readonly IRepository<EmbeddingLoadingState> _embeddingLoadingStatesRepository;
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;

        public WorkspaceDataObjectsAddPacksService(IRepository<WorkspaceDataObjectsAddPack> packsRepository,
            IRepository<EmbeddingLoadingState> embeddingLoadingStatesRepository,
            IRepository<ClusterizationWorkspace> workspacesRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IUserService userService,
            IMapper mapper,
            IEmbeddingLoadingStatesService embeddingLoadingStatesService)
        {
            _packsRepository = packsRepository;
            _embeddingLoadingStatesRepository = embeddingLoadingStatesRepository;
            _workspacesRepository = workspacesRepository;
            _localizer = localizer;
            _userService = userService;
            _mapper = mapper;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
        }

        public async Task<WorkspaceDataObjectsAddPackSimpleDTO> GetSimplePackById(int id)
        {
            var pack = (await _packsRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.Owner)},{nameof(WorkspaceDataObjectsAddPack.Workspace)}")).FirstOrDefault();

            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == pack.WorkspaceId)).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            if (workspace.ChangingType == ChangingTypes.OnlyOwner)
            {
                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (workspace.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceVisibleTypeIsOnlyOwner], HttpStatusCode.BadRequest);
            }



            return _mapper.Map<WorkspaceDataObjectsAddPackSimpleDTO>(pack);
        }
        public async Task<ICollection<WorkspaceDataObjectsAddPackSimpleDTO>> GetCustomerSimplePacks(GetCustomerWorkspaceDataObjectsAddPacksRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var packs = await _packsRepository.GetAsync(e => e.OwnerId == userId,
                                            includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.Owner)},{nameof(WorkspaceDataObjectsAddPack.Workspace)}",
                                            orderBy: o => o.OrderByDescending(e => e.CreationTime),
                                            pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<WorkspaceDataObjectsAddPackSimpleDTO>>(packs);
        }

        public async Task<ICollection<WorkspaceDataObjectsAddPackSimpleDTO>> GetSimplePacks(GetWorkspaceDataObjectsAddPacksRequest request)
        {
            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == request.WorkspaceId)).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            if (workspace.ChangingType == ChangingTypes.OnlyOwner)
            {
                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if (workspace.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceVisibleTypeIsOnlyOwner], HttpStatusCode.BadRequest);
            }

            var packs = await _packsRepository.GetAsync(e => e.WorkspaceId == request.WorkspaceId,
                                                        includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.Owner)},{nameof(WorkspaceDataObjectsAddPack.Workspace)}",
                                                        orderBy: o => o.OrderByDescending(e => e.CreationTime),
                                                        pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<WorkspaceDataObjectsAddPackSimpleDTO>>(packs);
        }

        public async Task<WorkspaceDataObjectsAddPackFullDTO> GetFullPack(int id)
        {
            var pack = (await _packsRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.Owner)},{nameof(WorkspaceDataObjectsAddPack.EmbeddingLoadingStates)},{nameof(WorkspaceDataObjectsAddPack.Workspace)}")).FirstOrDefault();

            if (pack == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceDataObjectsAddPackNotFound], HttpStatusCode.NotFound);

            
            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == pack.WorkspaceId)).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);

            if (workspace.ChangingType == ChangingTypes.OnlyOwner)
            {
                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

                if(workspace.OwnerId!=userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceVisibleTypeIsOnlyOwner], HttpStatusCode.BadRequest);
            }

            var statuses = await _embeddingLoadingStatesRepository.GetAsync(e => e.AddPackId == pack.Id, includeProperties: $"{nameof(EmbeddingLoadingState.EmbeddingModel)}");

            var mappedPack = _mapper.Map<WorkspaceDataObjectsAddPackFullDTO>(pack);
            mappedPack.EmbeddingLoadingStates = _mapper.Map<ICollection<EmbeddingLoadingStateDTO>>(statuses);

            return mappedPack;
        }


        public async Task DeletePack(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);


            var pack = (await _packsRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.DataObjects)}")).FirstOrDefault();

            if (pack == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceDataObjectsAddPackNotFound], HttpStatusCode.NotFound);


            if (pack.IsDeleted) return;

            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == pack.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);


            if (pack.OwnerId != userId && workspace.ChangingType == ChangingTypes.OnlyOwner && workspace.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], HttpStatusCode.BadRequest);

            foreach (var dataObject in pack.DataObjects)
            {
                workspace.DataObjects.Remove(dataObject);
                workspace.EntitiesCount--;
            }

            pack.IsDeleted = true;
            pack.LastDeleteTime = DateTime.UtcNow;

            await _packsRepository.SaveChangesAsync();

            await _embeddingLoadingStatesService.ReviewStates(workspace.Id);
        }
        public async Task RestorePack(int id)
        {
            var userId = await _userService.GetCurrentUserId();

            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);


            var pack = (await _packsRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(WorkspaceDataObjectsAddPack.DataObjects)}")).FirstOrDefault();

            if (pack == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceDataObjectsAddPackNotFound], HttpStatusCode.NotFound);

            if (!pack.IsDeleted) return;

            var workspace = (await _workspacesRepository.GetAsync(e => e.Id == pack.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], HttpStatusCode.NotFound);


            if (pack.OwnerId != userId && workspace.ChangingType == ChangingTypes.OnlyOwner && workspace.OwnerId != userId) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceChangingTypeIsOnlyOwner], HttpStatusCode.BadRequest);

            foreach (var dataObject in pack.DataObjects)
            {
                workspace.DataObjects.Add(dataObject);
                workspace.EntitiesCount++;
            }

            pack.IsDeleted = false;
            pack.LastDeleteTime = DateTime.UtcNow;

            await _packsRepository.SaveChangesAsync();

            await _embeddingLoadingStatesService.ReviewStates(workspace.Id);
        }
    }
}

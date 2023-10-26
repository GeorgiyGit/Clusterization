using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Embeddings;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Tasks;
using Domain.LoadHelpModels;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization
{
    public class ClusterizationWorkspacesService : IClusterizationWorkspacesService
    {
        private readonly IRepository<ClusterizationWorkspace> repository;
        private readonly IRepository<Comment> comments_repository;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IMyTasksService taskService;
        public ClusterizationWorkspacesService(IRepository<ClusterizationWorkspace> repository,
                                               IStringLocalizer<ErrorMessages> localizer,
                                               IMapper mapper,
                                               IRepository<Comment> comments_repository,
                                               IBackgroundJobClient backgroundJobClient,
                                               IMyTasksService taskService)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
            this.comments_repository = comments_repository;
            this.backgroundJobClient = backgroundJobClient;
            this.taskService = taskService;
        }

        #region add-update
        public async Task Add(AddClusterizationWorkspaceDTO model)
        {
            var newWorkspace = new ClusterizationWorkspace()
            {
                Title = model.Title,
                Description = model.Description,
                TypeId = model.TypeId
            };

            await repository.AddAsync(newWorkspace);
            await repository.SaveChangesAsync();
        }
        public async Task Update(UpdateClusterizationWorkspaceDTO model)
        {
            var workspace = (await repository.GetAsync(c => c.Id == model.Id, includeProperties: $"{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            workspace.Title = model.Title;
            workspace.Description = model.Description;

            await repository.SaveChangesAsync();
        }
        #endregion

        #region get
        public async Task<ClusterizationWorkspaceDTO> GetFullById(int id)
        {
            var workspace = (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)},{nameof(ClusterizationWorkspace.Profiles)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();

            if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            return mapper.Map<ClusterizationWorkspaceDTO>(workspace);
        }
        public async Task<ICollection<SimpleClusterizationWorkspaceDTO>> GetCollection(GetWorkspacesRequest request)
        {
            Expression<Func<ClusterizationWorkspace, bool>> filterCondition = e => true;


            if (request.FilterStr != null && request.FilterStr!="")
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => (e.TypeId == request.TypeId) && e.Title.Contains(request.FilterStr);
                }
                else
                {
                    filterCondition = e => e.Title.Contains(request.FilterStr);
                }
            }
            else 
            {
                if (request.TypeId != null)
                {
                    filterCondition = e => (e.TypeId == request.TypeId);
                }
            }

            var pageParameters = request.PageParameters;
            var workspaces = (await repository.GetAsync(filterCondition,includeProperties:$"{nameof(ClusterizationWorkspace.Type)}"))
                                              .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                              .Take(pageParameters.PageSize).ToList();

            return mapper.Map<ICollection<SimpleClusterizationWorkspaceDTO>>(workspaces);
        }
        #endregion

        #region add-entity
        public async Task LoadCommentsByChannel(AddCommentsToWorkspaceByChannelRequest request)
        {
            backgroundJobClient.Enqueue(() => LoadCommentsByChannelBackgroundJob(request));
        }
        public async Task LoadCommentsByChannelBackgroundJob(AddCommentsToWorkspaceByChannelRequest request)
        {
            var taskId = await taskService.CreateTask("Додавання коментарів");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await repository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)}")).FirstOrDefault();

                if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

                Expression<Func<Comment, bool>> filterCondition = e => true;

                if (request.DateFrom != null || request.DateTo != null)
                {
                    if (request.IsVideoDateCount)
                    {
                        if (request.DateFrom != null && request.DateTo != null)
                        {
                            filterCondition = e => e.Video.PublishedAtDateTimeOffset > request.DateFrom && e.Video.PublishedAtDateTimeOffset < request.DateTo;
                        }
                        else if (request.DateFrom != null)
                        {
                            filterCondition = e => e.Video.PublishedAtDateTimeOffset > request.DateFrom;
                        }
                        else
                        {
                            filterCondition = e => e.Video.PublishedAtDateTimeOffset < request.DateTo;
                        }
                    }
                    else
                    {
                        if (request.DateFrom != null && request.DateTo != null)
                        {
                            filterCondition = e => e.PublishedAtDateTimeOffset > request.DateFrom && e.PublishedAtDateTimeOffset < request.DateTo;
                        }
                        else if (request.DateFrom != null)
                        {
                            filterCondition = e => e.PublishedAtDateTimeOffset > request.DateFrom;
                        }
                        else
                        {
                            filterCondition = e => e.PublishedAtDateTimeOffset < request.DateTo;
                        }
                    }
                }

                var comments = (await comments_repository.GetAsync(filter: filterCondition, includeProperties: $"{nameof(Comment.Workspaces)}")).Take(request.MaxCount);

                foreach (var comment in comments)
                {
                    if (!workspace.Comments.Contains(comment))
                    {
                        workspace.Comments.Add(comment);
                        comment.Workspaces.Add(workspace);
                    }
                }

                workspace.IsAllDataEmbedded = false;

                await repository.SaveChangesAsync();

                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
        }

        public async Task LoadCommentsByVideos(AddCommentsToWorkspaceByVideosRequest request)
        {
            backgroundJobClient.Enqueue(() => LoadCommentsByVideosBackgroundJob(request));
        }
        public async Task LoadCommentsByVideosBackgroundJob(AddCommentsToWorkspaceByVideosRequest request)
        {
            var taskId = await taskService.CreateTask("Додавання коментарів");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var workspace = (await repository.GetAsync(c => c.Id == request.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.Comments)}")).FirstOrDefault();

                if (workspace == null) throw new HttpException(localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

                Expression<Func<Comment, bool>> filterCondition = e => true;

                if (request.DateFrom != null || request.DateTo != null)
                {
                    if (request.DateFrom != null && request.DateTo != null)
                    {
                        filterCondition = e => e.PublishedAtDateTimeOffset > request.DateFrom && e.PublishedAtDateTimeOffset < request.DateTo;
                    }
                    else if (request.DateFrom != null)
                    {
                        filterCondition = e => e.PublishedAtDateTimeOffset > request.DateFrom;
                    }
                    else
                    {
                        filterCondition = e => e.PublishedAtDateTimeOffset < request.DateTo;
                    }
                }

                foreach (var id in request.VideoIds)
                {
                    var comments = (await comments_repository.GetAsync(c => c.Video.Id == id, includeProperties: $"{nameof(Comment.Video)},{nameof(Comment.Workspaces)}")).Take(request.MaxCountInVideo);

                    foreach (var comment in comments)
                    {
                        if (!workspace.Comments.Contains(comment))
                        {
                            workspace.Comments.Add(comment);
                            comment.Workspaces.Add(workspace);
                        }
                    }
                }

                workspace.IsAllDataEmbedded = false;

                await repository.SaveChangesAsync();

                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
        }
        #endregion
       
    }
}

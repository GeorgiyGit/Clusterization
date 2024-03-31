using AutoMapper;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.Clusterization;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Interfaces.Youtube;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Domain.Services.TaskServices;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domain.Services.Youtube
{
    public class YoutubeCommentsService : IYoutubeCommentsService
    {
        private readonly IRepository<Entities.Youtube.Comment> repository;
        private readonly IRepository<Entities.Youtube.Video> videos_repository;

        private readonly YouTubeService youtubeService;
        private readonly IPrivateYoutubeChannelsService privateChannelService;
        private readonly IPrivateYoutubeVideosService privateVideoService;
        private readonly IYoutubeVideoService videoService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;
        private readonly IMyTasksService taskService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public YoutubeCommentsService(IRepository<Entities.Youtube.Comment> repository,
                                      IConfiguration configuration,
                                      IPrivateYoutubeChannelsService privateChannelService,
                                      IPrivateYoutubeVideosService privateVideoService,
                                      IYoutubeVideoService videoService,
                                      IMapper mapper,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMyTasksService taskService,
                                      IBackgroundJobClient backgroundJobClient,
                                      IRepository<Entities.Youtube.Video> videos_repository,
                                      IUserService userService,
                                      IStringLocalizer<TaskTitles> tasksLocalizer,
                                      IQuotasControllerService quotasControllerService)
        {
            this.repository = repository;
            this.privateChannelService = privateChannelService;
            this.privateVideoService = privateVideoService;
            this.videoService = videoService;
            this.mapper = mapper;
            this.localizer = localizer;
            this.taskService = taskService;
            this.backgroundJobClient = backgroundJobClient;
            this.videos_repository = videos_repository;
            _userService = userService;
            _quotasControllerService = quotasControllerService;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = youtubeOptions["ApiKey"],
                ApplicationName = youtubeOptions["ApplicationName"]
            });
            _tasksLocalizer = tasksLocalizer;
        }

        #region load
        public async Task LoadFromVideo(LoadOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);
            
            var taskId = await taskService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingCommentsFromYoutube]);

            backgroundJobClient.Enqueue(() => LoadCommentsFromVideoBackgroundJob(options,userId, taskId));
        }
        public async Task LoadCommentsFromVideoBackgroundJob(LoadOptions options, string userId, int taskId)
        {
            string videoId = options.ParentId;
            bool isNewVideo = false;
            if ((await privateVideoService.GetById(videoId)) == null)
            {
                await videoService.LoadById(videoId);
                isNewVideo = true;
            }

            var stateId = await taskService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;

            var logsId = Guid.NewGuid().ToString();
            try
            {
                var nextPageToken = "";
                int newCommentCount = 0;

                var video = await privateVideoService.GetById(videoId);
                var channel = await privateChannelService.GetById(video.ChannelId);
                for (int i = 0; i < options.MaxLoad;)
                {
                    var searchRequest = youtubeService.CommentThreads.List("snippet");

                    // Set the channelId to load videos from the specific channel
                    searchRequest.VideoId = videoId;

                    // Set the order to retrieve videos by date (you can change this as needed)
                    searchRequest.Order = CommentThreadsResource.ListRequest.OrderEnum.Time;

                    if (nextPageToken != null && nextPageToken != "") searchRequest.PageToken = nextPageToken;

                    searchRequest.MaxResults = 200;

                    var response = searchRequest.Execute();

                    nextPageToken = response.NextPageToken;

                    var comments = response.Items;

                    foreach (var comment in comments)
                    {
                        if (i >= options.MaxLoad) break;
                        if (options.DateFrom != null)
                        {
                            if (comment.Snippet.TopLevelComment.Snippet.PublishedAt < options.DateFrom)
                            {
                                i = options.MaxLoad;
                                break;
                            }
                        }
                        if (options.DateTo != null)
                        {
                            if (comment.Snippet.TopLevelComment.Snippet.PublishedAt > options.DateTo)
                            {
                                continue;
                            }
                        }

                        if (!isNewVideo)
                        {
                            if (await repository.FindAsync(comment.Id) != null)
                            {
                                continue;
                            }
                        }

                        var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, 1, logsId);

                        if (!quotasResult)
                        {
                            await taskService.ChangeTaskState(taskId, TaskStates.Error);
                            await taskService.ChangeTaskDescription(taskId, localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                            return;
                        }

                        var newComment = new Entities.Youtube.Comment()
                        {
                            Id = comment.Id,
                            ETag = comment.ETag,
                            CanReply = (bool)comment.Snippet.CanReply,
                            ChannelId = comment.Snippet.ChannelId,
                            TotalReplyCount = (short)comment.Snippet.TotalReplyCount,
                            VideoId = comment.Snippet.VideoId,

                            AuthorChannelId = comment.Snippet.TopLevelComment.Snippet.AuthorChannelId.Value,
                            AuthorDisplayName = comment.Snippet.TopLevelComment.Snippet.AuthorDisplayName,
                            AuthorChannelUrl = comment.Snippet.TopLevelComment.Snippet.AuthorChannelUrl,
                            AuthorProfileImageUrl = comment.Snippet.TopLevelComment.Snippet.AuthorProfileImageUrl,
                            LikeCount = (int)comment.Snippet.TopLevelComment.Snippet.LikeCount,
                            PublishedAt = (DateTime)comment.Snippet.TopLevelComment.Snippet.PublishedAt,
                            PublishedAtDateTimeOffset = (DateTimeOffset)comment.Snippet.TopLevelComment.Snippet.PublishedAtDateTimeOffset,
                            PublishedAtRaw = comment.Snippet.TopLevelComment.Snippet.PublishedAtRaw,
                            TextDisplay = comment.Snippet.TopLevelComment.Snippet.TextDisplay,
                            TextOriginal = comment.Snippet.TopLevelComment.Snippet.TextOriginal,
                            UpdatedAt = (DateTime)comment.Snippet.TopLevelComment.Snippet.UpdatedAt,
                            UpdatedAtDateTimeOffset = (DateTimeOffset)comment.Snippet.TopLevelComment.Snippet.UpdatedAtDateTimeOffset,
                            UpdatedAtRaw = comment.Snippet.TopLevelComment.Snippet.UpdatedAtRaw
                        };

                        newComment.LoaderId = userId;

                        await repository.AddAsync(newComment);
                        i++;

                        float plusPercent = 100f / options.MaxLoad;
                        percent += plusPercent;


                        if (video != null)
                        {
                            video.LoadedCommentCount += 1;

                            if (channel != null)
                            {
                                channel.LoadedCommentCount += 1;
                            }
                        }

                        await taskService.ChangeTaskPercent(taskId, percent);
                    }

                    await repository.SaveChangesAsync();

                    if (nextPageToken == "" || nextPageToken == null) break;
                }

                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
                await taskService.ChangeTaskDescription(taskId, ex.Message);
            }
        }


        public async Task LoadFromChannel(LoadCommentsByChannelOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var taskId = await taskService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingCommentsFromYoutube]);

            backgroundJobClient.Enqueue(() => LoadCommentsFromChannelBackgroundJob(options, userId, taskId));
        }
        public async Task LoadCommentsFromChannelBackgroundJob(LoadCommentsByChannelOptions options, string userId, int taskId)
        {
            string channelId= options.ParentId;
            if ((await privateChannelService.GetById(channelId)) == null) return;

            var stateId = await taskService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;

            var logsId = Guid.NewGuid().ToString();
            try
            {
                Expression<Func<Entities.Youtube.Video, bool>> filterCondition = e => true;

                if (options.IsVideoDateCount == true)
                {
                    if (options.DateFrom != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset > options.DateFrom);
                    if (options.DateTo != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset < options.DateTo);
                }
                Random random = new Random();

                var videos = await videos_repository.GetAsync(filter: filterCondition, pageParameters: null);
                List<Entities.Youtube.Video> shuffledVideos = videos.OrderBy(x => random.Next()).ToList();
                
                int loadedCount = 0;
                foreach (var video in shuffledVideos)
                {
                    var nextPageToken = "";
                    int loadedCountInOneVideo = 0;
                    while (true)
                    {
                        int newCommentCount = 0;
                        var searchRequest = youtubeService.CommentThreads.List("snippet");

                        // Set the channelId to load videos from the specific channel
                        searchRequest.VideoId = video.Id;

                        // Set the order to retrieve videos by date (you can change this as needed)
                        searchRequest.Order = CommentThreadsResource.ListRequest.OrderEnum.Time;

                        if (nextPageToken != null && nextPageToken != "") searchRequest.PageToken = nextPageToken;

                        searchRequest.MaxResults = 200;

                        var response = searchRequest.Execute();

                        nextPageToken = response.NextPageToken;

                        var comments = response.Items;

                        foreach (var comment in comments)
                        {
                            if (loadedCount >= options.MaxLoad) break;
                            if(loadedCountInOneVideo>=options.MaxLoadForOneVideo) break;

                            if (options.IsVideoDateCount == false)
                            {
                                if (options.DateFrom != null)
                                {
                                    if (comment.Snippet.TopLevelComment.Snippet.PublishedAt < options.DateFrom)
                                    {
                                        loadedCount = options.MaxLoad;
                                        break;
                                    }
                                }
                                if (options.DateTo != null)
                                {
                                    if (comment.Snippet.TopLevelComment.Snippet.PublishedAt > options.DateTo)
                                    {
                                        continue;
                                    }
                                }
                            }

                            if (await repository.FindAsync(comment.Id) != null)
                            {
                                continue;
                            }

                            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, 1, logsId);

                            if (!quotasResult)
                            {
                                await taskService.ChangeTaskState(taskId, TaskStates.Error);
                                await taskService.ChangeTaskDescription(taskId, localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                                return;
                            }

                            var newComment = new Entities.Youtube.Comment()
                            {
                                Id = comment.Id,
                                ETag = comment.ETag,
                                CanReply = (bool)comment.Snippet.CanReply,
                                ChannelId = comment.Snippet.ChannelId,
                                TotalReplyCount = (short)comment.Snippet.TotalReplyCount,
                                VideoId = comment.Snippet.VideoId,
                                
                                AuthorDisplayName = comment.Snippet.TopLevelComment.Snippet.AuthorDisplayName,
                                AuthorChannelUrl = comment.Snippet.TopLevelComment.Snippet.AuthorChannelUrl,
                                AuthorProfileImageUrl = comment.Snippet.TopLevelComment.Snippet.AuthorProfileImageUrl,
                                LikeCount = (int)comment.Snippet.TopLevelComment.Snippet.LikeCount,
                                PublishedAt = (DateTime)comment.Snippet.TopLevelComment.Snippet.PublishedAt,
                                PublishedAtDateTimeOffset = (DateTimeOffset)comment.Snippet.TopLevelComment.Snippet.PublishedAtDateTimeOffset,
                                PublishedAtRaw = comment.Snippet.TopLevelComment.Snippet.PublishedAtRaw,
                                TextDisplay = comment.Snippet.TopLevelComment.Snippet.TextDisplay,
                                TextOriginal = comment.Snippet.TopLevelComment.Snippet.TextOriginal,
                                UpdatedAt = (DateTime)comment.Snippet.TopLevelComment.Snippet.UpdatedAt,
                                UpdatedAtDateTimeOffset = (DateTimeOffset)comment.Snippet.TopLevelComment.Snippet.UpdatedAtDateTimeOffset,
                                UpdatedAtRaw = comment.Snippet.TopLevelComment.Snippet.UpdatedAtRaw
                            };
                            newComment.LoaderId = userId;

                            if (comment.Snippet.TopLevelComment.Snippet.AuthorChannelId != null)
                            {
                                newComment.AuthorChannelId = comment.Snippet.TopLevelComment.Snippet.AuthorChannelId.Value;
                            }
                            else
                            {
                                newComment.AuthorChannelId = "";
                            }

                            await repository.AddAsync(newComment);
                            loadedCount++;
                            loadedCountInOneVideo++;

                            float plusPercent = 100f / options.MaxLoad;
                            percent += plusPercent;

                            await taskService.ChangeTaskPercent(taskId, percent);
                            newCommentCount++;
                        }

                        video.LoadedCommentCount += newCommentCount;

                        var channel = await privateChannelService.GetById(video.ChannelId);
                        if (channel != null)
                        {
                            channel.LoadedCommentCount += newCommentCount;
                        }

                        await repository.SaveChangesAsync();

                        if (nextPageToken == "" || nextPageToken == null || loadedCount >= options.MaxLoad || loadedCountInOneVideo>=options.MaxLoadForOneVideo) break;
                    }


                    if (loadedCount >= options.MaxLoad) break;
                }

                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
                await taskService.ChangeTaskDescription(taskId, ex.Message);
            }
        }
        #endregion

        #region get
        public async Task<CommentDTO> GetLoadedById(string commentId)
        {
            var comment = (await repository.GetAsync(c => c.Id == commentId, includeProperties: $"{nameof(Entities.Youtube.Comment.Video)},{nameof(Entities.Youtube.Comment.Channel)}", pageParameters: null)).FirstOrDefault();

            if (comment == null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeCommentNotFound], System.Net.HttpStatusCode.NotFound);

            return mapper.Map<CommentDTO>(comment);
        }
        public async Task<ICollection<CommentDTO>> GetLoadedCollection(GetCommentsRequest request)
        {
            if (request.FilterStr != null && request.FilterStr != "")
            {
                try
                {
                    var video = await GetLoadedById(request.FilterStr);

                    return new List<CommentDTO>() { video };
                }
                catch { }
            }
            Expression<Func<Entities.Youtube.Comment, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.TextOriginal.Contains(request.FilterStr);
            if(request.VideoId != null && request.VideoId != "")
            {
                filterCondition = e => e.VideoId == request.VideoId;
            }
            else if (request.ChannelId != null && request.ChannelId != "")
            {
                filterCondition = e => e.ChannelId == request.ChannelId;
            }


            Func<IQueryable<Entities.Youtube.Comment>, IOrderedQueryable<Entities.Youtube.Comment>> orderByExpression = q =>
                q.OrderByDescending(e => e.PublishedAtDateTimeOffset);

            if (request.FilterType == CommentFilterTypes.Time)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.PublishedAtDateTimeOffset);
            }
            else if (request.FilterType == CommentFilterTypes.Relevance)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.LikeCount);
            }
            else
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.Id);
            }

            var pageParameters = request.PageParameters;

            var comments = (await repository.GetAsync(filter: filterCondition,
                                                   includeProperties: $"{nameof(Entities.Youtube.Comment.Video)},{nameof(Entities.Youtube.Comment.Channel)}",
                                                   orderBy: orderByExpression,
                                                   pageParameters: pageParameters));

            return mapper.Map<ICollection<CommentDTO>>(comments);
        }
        #endregion
    }
}

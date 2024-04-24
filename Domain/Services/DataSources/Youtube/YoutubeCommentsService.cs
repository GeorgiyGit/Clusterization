﻿using AutoMapper;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using Domain.Resources.Types.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class YoutubeCommentsService : IYoutubeCommentsService
    {
        private readonly IRepository<Entities.DataSources.Youtube.Comment> _repository;
        private readonly IRepository<Entities.DataSources.Youtube.Video> _videosRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;
        private readonly IBackgroundJobClient _backgroundJobClient;

        private readonly YouTubeService _youtubeService;
        private readonly IPrivateYoutubeChannelsService _privateChannelService;
        private readonly IPrivateYoutubeVideosService _privateVideoService;
        private readonly IYoutubeVideoService _videoService;
        private readonly IMapper _mapper;
        private readonly IMyTasksService _tasksService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public YoutubeCommentsService(IRepository<Entities.DataSources.Youtube.Comment> repository,
                                      IConfiguration configuration,
                                      IPrivateYoutubeChannelsService privateChannelService,
                                      IPrivateYoutubeVideosService privateVideoService,
                                      IYoutubeVideoService videoService,
                                      IMapper mapper,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMyTasksService tasksService,
                                      IBackgroundJobClient backgroundJobClient,
                                      IRepository<Entities.DataSources.Youtube.Video> videosRepository,
                                      IUserService userService,
                                      IStringLocalizer<TaskTitles> tasksLocalizer,
                                      IQuotasControllerService quotasControllerService)
        {
            _repository = repository;
            _privateChannelService = privateChannelService;
            _privateVideoService = privateVideoService;
            _videoService = videoService;
            _mapper = mapper;
            _localizer = localizer;
            _tasksService = tasksService;
            _backgroundJobClient = backgroundJobClient;
            _videosRepository = videosRepository;
            _userService = userService;
            _quotasControllerService = quotasControllerService;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
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
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingCommentsFromYoutube]);

            _backgroundJobClient.Enqueue(() => LoadCommentsFromVideoBackgroundJob(options, userId, taskId));
        }
        public async Task LoadCommentsFromVideoBackgroundJob(LoadOptions options, string userId, int taskId)
        {
            string videoId = options.ParentId;
            bool isNewVideo = false;
            if (await _privateVideoService.GetById(videoId) == null)
            {
                await _videoService.LoadById(videoId);
                isNewVideo = true;
            }

            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;

            var logsId = Guid.NewGuid().ToString();
            try
            {
                var nextPageToken = "";
                int newCommentCount = 0;

                var video = await _privateVideoService.GetById(videoId);
                var channel = await _privateChannelService.GetById(video.ChannelId);
                for (int i = 0; i < options.MaxLoad;)
                {
                    var searchRequest = _youtubeService.CommentThreads.List("snippet");

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
                            if (await _repository.FindAsync(comment.Id) != null)
                            {
                                continue;
                            }
                        }

                        var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, 1, logsId);

                        if (!quotasResult)
                        {
                            await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                            await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                            return;
                        }

                        var newComment = new Entities.DataSources.Youtube.Comment()
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

                        await _repository.AddAsync(newComment);
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

                        await _tasksService.ChangeTaskPercent(taskId, percent);
                    }

                    await _repository.SaveChangesAsync();

                    if (nextPageToken == "" || nextPageToken == null) break;
                }

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }


        public async Task LoadFromChannel(LoadCommentsByChannelOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingCommentsFromYoutube]);

            _backgroundJobClient.Enqueue(() => LoadCommentsFromChannelBackgroundJob(options, userId, taskId));
        }
        public async Task LoadCommentsFromChannelBackgroundJob(LoadCommentsByChannelOptions options, string userId, int taskId)
        {
            string channelId = options.ParentId;
            if (await _privateChannelService.GetById(channelId) == null) return;

            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;

            var logsId = Guid.NewGuid().ToString();
            try
            {
                Expression<Func<Entities.DataSources.Youtube.Video, bool>> filterCondition = e => true;

                if (options.IsVideoDateCount == true)
                {
                    if (options.DateFrom != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset > options.DateFrom);
                    if (options.DateTo != null) filterCondition = filterCondition.And(e => e.PublishedAtDateTimeOffset < options.DateTo);
                }
                Random random = new Random();

                var videos = await _videosRepository.GetAsync(filter: filterCondition, pageParameters: null);
                List<Entities.DataSources.Youtube.Video> shuffledVideos = videos.OrderBy(x => random.Next()).ToList();

                int loadedCount = 0;
                foreach (var video in shuffledVideos)
                {
                    var nextPageToken = "";
                    int loadedCountInOneVideo = 0;
                    while (true)
                    {
                        int newCommentCount = 0;
                        var searchRequest = _youtubeService.CommentThreads.List("snippet");

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
                            if (loadedCountInOneVideo >= options.MaxLoadForOneVideo) break;

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

                            if (await _repository.FindAsync(comment.Id) != null)
                            {
                                continue;
                            }

                            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, 1, logsId);

                            if (!quotasResult)
                            {
                                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                                await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                                return;
                            }

                            var newComment = new Entities.DataSources.Youtube.Comment()
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

                            await _repository.AddAsync(newComment);
                            loadedCount++;
                            loadedCountInOneVideo++;

                            float plusPercent = 100f / options.MaxLoad;
                            percent += plusPercent;

                            await _tasksService.ChangeTaskPercent(taskId, percent);
                            newCommentCount++;
                        }

                        video.LoadedCommentCount += newCommentCount;

                        var channel = await _privateChannelService.GetById(video.ChannelId);
                        if (channel != null)
                        {
                            channel.LoadedCommentCount += newCommentCount;
                        }

                        await _repository.SaveChangesAsync();

                        if (nextPageToken == "" || nextPageToken == null || loadedCount >= options.MaxLoad || loadedCountInOneVideo >= options.MaxLoadForOneVideo) break;
                    }


                    if (loadedCount >= options.MaxLoad) break;
                }

                await _tasksService.ChangeTaskPercent(taskId, 100f);
                await _tasksService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch (Exception ex)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, ex.Message);
            }
        }
        #endregion

        #region get
        public async Task<CommentDTO> GetLoadedById(string commentId)
        {
            var comment = (await _repository.GetAsync(c => c.Id == commentId, includeProperties: $"{nameof(Entities.DataSources.Youtube.Comment.Video)},{nameof(Entities.DataSources.Youtube.Comment.Channel)}", pageParameters: null)).FirstOrDefault();

            if (comment == null) throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeCommentNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<CommentDTO>(comment);
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
            Expression<Func<Entities.DataSources.Youtube.Comment, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.TextOriginal.Contains(request.FilterStr);
            if (request.VideoId != null && request.VideoId != "")
            {
                filterCondition = e => e.VideoId == request.VideoId;
            }
            else if (request.ChannelId != null && request.ChannelId != "")
            {
                filterCondition = e => e.ChannelId == request.ChannelId;
            }


            Func<IQueryable<Entities.DataSources.Youtube.Comment>, IOrderedQueryable<Entities.DataSources.Youtube.Comment>> orderByExpression = q =>
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

            var comments = await _repository.GetAsync(filter: filterCondition,
                                                   includeProperties: $"{nameof(Entities.DataSources.Youtube.Comment.Video)},{nameof(Entities.DataSources.Youtube.Comment.Channel)}",
                                                   orderBy: orderByExpression,
                                                   pageParameters: pageParameters);

            return _mapper.Map<ICollection<CommentDTO>>(comments);
        }
        #endregion
    }
}

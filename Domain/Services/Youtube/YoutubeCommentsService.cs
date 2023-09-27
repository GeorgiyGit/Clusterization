using AutoMapper;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Tasks;
using Domain.Interfaces.Youtube;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Services.TaskServices;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domain.Services.Youtube
{
    public class YoutubeCommentsService : IYoutubeCommentsService
    {
        private readonly IRepository<Entities.Youtube.Comment> repository;
        private readonly YouTubeService youtubeService;
        private readonly IPrivateYoutubeChannelService privateChannelService;
        private readonly IPrivateYoutubeVideoService privateVideoService;
        private readonly IYoutubeVideoService videoService;
        private readonly IYoutubeChannelService channelService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMyTaskService taskService;
        public YoutubeCommentsService(IRepository<Entities.Youtube.Comment> repository,
                                      IConfiguration configuration,
                                      IPrivateYoutubeChannelService privateChannelService,
                                      IPrivateYoutubeVideoService privateVideoService,
                                      IYoutubeVideoService videoService,
                                      IYoutubeChannelService channelService,
                                      IMapper mapper,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMyTaskService taskService)
        {
            this.repository = repository;
            this.privateChannelService = privateChannelService;
            this.privateVideoService = privateVideoService;
            this.videoService = videoService;
            this.channelService = channelService;
            this.mapper = mapper;
            this.localizer = localizer;
            this.taskService = taskService;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = youtubeOptions["ApiKey"],
                ApplicationName = youtubeOptions["ApplicationName"]
            });
        }

        #region load
        public async Task LoadCommentsFromVideo(LoadOptions options)
        {
            string videoId = options.ParentId;
            bool isNewVideo = false;
            if ((await privateVideoService.GetById(videoId)) == null)
            {
                await videoService.LoadVideoById(videoId);
                isNewVideo = true;
            }

            var taskId = await taskService.CreateTask("Завантаження відео");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var nextPageToken = "";
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
                                break;
                            }
                        }

                        if (!isNewVideo)
                        {
                            if (await repository.FindAsync(comment.Id) != null)
                            {
                                continue;
                            }
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

                        await repository.AddAsync(newComment);
                        i++;

                        float plusPercent = 100 / options.MaxLoad;
                        percent += plusPercent;

                        await taskService.ChangeTaskPercent(taskId, percent);
                    }
                    await repository.SaveChangesAsync();

                    if (nextPageToken == "" || nextPageToken == null) break;
                }
                await taskService.ChangeTaskPercent(taskId, 100f);
                await taskService.ChangeTaskState(taskId, TaskStates.Completed);
            }
            catch
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
            }
        }
        #endregion

        #region get
        public async Task<CommentDTO> GetLoadedCommentById(string commentId)
        {
            var comment = (await repository.GetAsync(c => c.Id == commentId, includeProperties: $"{nameof(Entities.Youtube.Comment.Video)},{nameof(Entities.Youtube.Comment.Channel)}")).FirstOrDefault();

            if (comment == null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeCommentNotFound], System.Net.HttpStatusCode.NotFound);

            return mapper.Map<CommentDTO>(comment);
        }
        public async Task<ICollection<CommentDTO>> GetLoadedComments(GetCommentsRequest request)
        {
            if (request.FilterStr != null && request.FilterStr != "")
            {
                try
                {
                    var video = await GetLoadedCommentById(request.FilterStr);

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
                                                   orderBy: orderByExpression))
                                          .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                          .Take(pageParameters.PageSize).ToList();

            return mapper.Map<ICollection<CommentDTO>>(comments);
        }
        #endregion
    }
}

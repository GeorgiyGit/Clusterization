using Domain.Interfaces.Youtube;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using AutoMapper;
using Domain.Resources.Types;
using System.Security.Cryptography;
using System.Threading.Channels;
using System.Linq.Expressions;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Hangfire;
using Domain.Interfaces.Tasks;
using System.Net;
using Hangfire.States;
using System.Xml.Linq;
using Hangfire.Common;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Domain.Services.Youtube
{
    public class YoutubeVideoService : IYoutubeVideoService
    {
        private readonly IRepository<Entities.Youtube.Video> repository;
        private readonly YouTubeService youtubeService;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IPrivateYoutubeChannelService privateChannelService;
        private readonly IYoutubeChannelService youtubeChannelService;
        private readonly IMapper mapper;
        private readonly IMyTaskService taskService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private const string QUEUE_NAME = "load";
        public YoutubeVideoService(IRepository<Entities.Youtube.Video> repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IPrivateYoutubeChannelService privateChannelService,
                                     IYoutubeChannelService youtubeChannelService,
                                     IMapper mapper,
                                     IMyTaskService taskService,
                                     IBackgroundJobClient backgroundJobClient)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.privateChannelService = privateChannelService;
            this.youtubeChannelService = youtubeChannelService;
            this.mapper = mapper;
            this.taskService = taskService;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = youtubeOptions["ApiKey"],
                ApplicationName = youtubeOptions["ApplicationName"]
            });
            this.backgroundJobClient = backgroundJobClient;
        }

        #region load
        public async Task LoadChannelVideosHangFire(DTOs.YoutubeDTOs.Requests.LoadOptions options)
        {
            backgroundJobClient.Enqueue(() => LoadChannelVideos(options));
        }
        public async Task LoadChannelVideos(DTOs.YoutubeDTOs.Requests.LoadOptions options)
        {
            string channelId = options.ParentId;

            bool isNewChannel = false;
            if ((await privateChannelService.GetById(channelId)) == null)
            {
                await youtubeChannelService.LoadChannel(channelId);
                isNewChannel = true;
            }

            var taskId = await taskService.CreateTask("Завантаження відео");
            float percent = 0f;

            await taskService.ChangeTaskState(taskId, TaskStates.Process);

            try
            {
                var nextPageToken = "";

                for (int i = 0; i < options.MaxLoad;)
                {
                    var searchRequest = youtubeService.PlaylistItems.List("snippet");

                    var newChannelId = "UU" + channelId.Remove(0, 2);
                    // Set the channelId to load videos from the specific channel
                    searchRequest.PlaylistId = newChannelId;

                    // Set the order to retrieve videos by date (you can change this as needed)
                    //searchRequest. = SearchResource.ListRequest.OrderEnum.Date;

                    if (nextPageToken != null && nextPageToken != "") searchRequest.PageToken = nextPageToken;

                    searchRequest.MaxResults = 100;

                    // Execute the request
                    var searchResponse = searchRequest.Execute();

                    nextPageToken = searchResponse.NextPageToken;

                    var videoIds = searchResponse.Items.Select(item => item.Snippet.ResourceId.VideoId).ToList();

                    // Create the videos request to retrieve video statistics and tags
                    var videosRequest = youtubeService.Videos.List("snippet,statistics");

                    videosRequest.MaxResults = 100;
                    // Set the video IDs to load statistics and tags for the specific videos
                    videosRequest.Id = string.Join(",", videoIds);

                    // Execute the request and return the list of videos
                    var videosResponse = videosRequest.Execute();

                    var videos = videosResponse.Items;

                    foreach (var video in videos)
                    {
                        if (i >= options.MaxLoad) break;
                        if (options.DateFrom != null)
                        {
                            if (video.Snippet.PublishedAt < options.DateFrom)
                            {
                                i = options.MaxLoad;
                                break;
                            }
                        }
                        if (options.DateTo != null)
                        {
                            if (video.Snippet.PublishedAt > options.DateTo)
                            {
                                break;
                            }
                        }

                        if (!isNewChannel)
                        {
                            if (await repository.FindAsync(video.Id) != null)
                            {
                                continue;
                            }
                        }
                        if (options.MinCommentCount != null)
                        {
                            if (video.Statistics.CommentCount == null && options.MinCommentCount > 0) continue;
                            else if (video.Statistics.CommentCount != null)
                            {
                                if (options.MinCommentCount > video.Statistics.CommentCount)
                                {
                                    continue;
                                }
                            }
                        }

                        var newVideo = new Entities.Youtube.Video()
                        {
                            Id = video.Id,
                            ETag = video.ETag,
                            CategoryId = video.Snippet.CategoryId,
                            ChannelId = video.Snippet.ChannelId,
                            ChannelTitle = video.Snippet.ChannelTitle,
                            ViewCount = (long)video.Statistics.ViewCount,
                            Title = video.Snippet.Title,
                            LoadedDate = DateTime.UtcNow,
                            Description = video.Snippet.Description,
                            LiveBroadcaseContent = video.Snippet.LiveBroadcastContent,
                            DefaultLanguage = video.Snippet.DefaultLanguage,
                            DefaultAudioLanguage = video.Snippet.DefaultAudioLanguage,
                            PublishedAt = (DateTime)video.Snippet.PublishedAt,
                            PublishedAtDateTimeOffset = (DateTimeOffset)video.Snippet.PublishedAtDateTimeOffset,
                            PublishedAtRaw = video.Snippet.PublishedAtRaw,
                            VideoImageUrl = video.Snippet.Thumbnails.Medium.Url
                        };

                        if (video.Statistics.LikeCount != null)
                        {
                            newVideo.LikeCount = (int)video.Statistics.LikeCount;
                        }

                        if (video.Statistics.CommentCount != null)
                        {
                            newVideo.CommentCount = (int)video.Statistics.CommentCount;
                        }

                        await repository.AddAsync(newVideo);
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
        public async Task LoadVideoById(string id)
        {
            if (await repository.FindAsync(id) != null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeVideoAlreadyLoaded], System.Net.HttpStatusCode.Conflict);

            var videosRequest = youtubeService.Videos.List("snippet,statistics");

            // Set the channelId to load the specific channel
            videosRequest.Id = id;

            var response = new VideoListResponse();
            try
            {
                response = videosRequest.Execute();
            }
            catch
            {
                throw new HttpException(localizer[ErrorMessagePatterns.YoutubeVideolLoadingError], System.Net.HttpStatusCode.BadRequest);
            }


            if (response.Items == null || response.Items.Count()==0) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeVideoDoesNotExist], System.Net.HttpStatusCode.NotFound);

            var video = response.Items[0];

            try
            {
                var newVideo = new Entities.Youtube.Video()
                {
                    Id = video.Id,
                    ETag = video.ETag,
                    CategoryId = video.Snippet.CategoryId,
                    ChannelId = video.Snippet.ChannelId,
                    ChannelTitle = video.Snippet.ChannelTitle,
                    ViewCount = (long)video.Statistics.ViewCount,
                    Title = video.Snippet.Title,
                    LoadedDate = DateTime.UtcNow,
                    Description = video.Snippet.Description,
                    LiveBroadcaseContent = video.Snippet.LiveBroadcastContent,
                    DefaultLanguage = video.Snippet.DefaultLanguage,
                    DefaultAudioLanguage = video.Snippet.DefaultAudioLanguage,
                    PublishedAt = (DateTime)video.Snippet.PublishedAt,
                    PublishedAtDateTimeOffset = (DateTimeOffset)video.Snippet.PublishedAtDateTimeOffset,
                    PublishedAtRaw = video.Snippet.PublishedAtRaw,
                    VideoImageUrl = video.Snippet.Thumbnails.Medium.Url
                };

                if (video.Statistics.LikeCount != null)
                {
                    newVideo.LikeCount = (int)video.Statistics.LikeCount;
                }

                if (video.Statistics.CommentCount != null)
                {
                    newVideo.CommentCount = (int)video.Statistics.CommentCount;
                }

                await repository.AddAsync(newVideo);
                await repository.SaveChangesAsync();
            }
            catch
            {
                throw new HttpException(localizer[ErrorMessagePatterns.YoutubeVideoAddingError], System.Net.HttpStatusCode.BadRequest);
            }
        }
        public async Task LoadManyByIds(ICollection<string> ids)
        {
            while (ids.Count() > 0)
            {
                // Create the videos request to retrieve video statistics and tags
                var videosRequest = youtubeService.Videos.List("snippet,statistics");

                videosRequest.MaxResults = 100;
                // Set the video IDs to load statistics and tags for the specific videos
                videosRequest.Id = string.Join(",", ids);

                // Execute the request and return the list of videos
                var videosResponse = videosRequest.Execute();

                var videos = videosResponse.Items;

                foreach (var video in videos)
                {
                    ids.Remove(video.Id);

                    if ((await repository.FindAsync(video.Id)) != null) continue;
                    var newVideo = new Entities.Youtube.Video()
                    {
                        Id = video.Id,
                        ETag = video.ETag,
                        CategoryId = video.Snippet.CategoryId,
                        ChannelId = video.Snippet.ChannelId,
                        ChannelTitle = video.Snippet.ChannelTitle,
                        ViewCount = (long)video.Statistics.ViewCount,
                        Title = video.Snippet.Title,
                        LoadedDate = DateTime.UtcNow,
                        Description = video.Snippet.Description,
                        LiveBroadcaseContent = video.Snippet.LiveBroadcastContent,
                        DefaultLanguage = video.Snippet.DefaultLanguage,
                        DefaultAudioLanguage = video.Snippet.DefaultAudioLanguage,
                        PublishedAt = (DateTime)video.Snippet.PublishedAt,
                        PublishedAtDateTimeOffset = (DateTimeOffset)video.Snippet.PublishedAtDateTimeOffset,
                        PublishedAtRaw = video.Snippet.PublishedAtRaw,
                        VideoImageUrl = video.Snippet.Thumbnails.Medium.Url
                    };

                    if (video.Statistics.LikeCount != null)
                    {
                        newVideo.LikeCount = (int)video.Statistics.LikeCount;
                    }

                    if (video.Statistics.CommentCount != null)
                    {
                        newVideo.CommentCount = (int)video.Statistics.CommentCount;
                    }

                    await repository.AddAsync(newVideo);
                }

                await repository.SaveChangesAsync();
            }
        }
        #endregion

        #region get
        public async Task<SimpleVideoDTO> GetLoadedVideoById(string id)
        {
            var video = (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Entities.Youtube.Video.Comments)},{nameof(Entities.Youtube.Video.Channel)}")).FirstOrDefault();

            if (video == null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeVideoNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedVideo = mapper.Map<SimpleVideoDTO>(video);
            mappedVideo.IsLoaded = true;

            return mappedVideo;
        }
        public async Task<ICollection<SimpleVideoDTO>> GetLoadedVideos(GetVideosRequest request)
        {
            if (request.FilterStr != null && request.FilterStr != "")
            {
                try
                {
                    var video = await GetLoadedVideoById(request.FilterStr);

                    return new List<SimpleVideoDTO>() { video };
                }
                catch { }
            }
            Expression<Func<Entities.Youtube.Video, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.Title.Contains(request.FilterStr);
            if (request.ChannelId != null && request.ChannelId != "")
            {
                var channel = await youtubeChannelService.GetLoadedChannelById(request.ChannelId);

                if (channel != null)
                {
                    filterCondition = e => e.ChannelId == request.ChannelId;
                }
            }


            Func<IQueryable<Entities.Youtube.Video>, IOrderedQueryable<Entities.Youtube.Video>> orderByExpression = q =>
                q.OrderByDescending(e => e.PublishedAtDateTimeOffset);

            if (request.FilterType == VideoFilterTypes.ByTimeDesc)
            {
                orderByExpression= q =>
                        q.OrderByDescending(e => e.PublishedAtDateTimeOffset);
            }
            else if (request.FilterType == VideoFilterTypes.ByTimeInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.PublishedAtDateTimeOffset);
            }
            else if (request.FilterType == VideoFilterTypes.ByViewsDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.ViewCount);
            }
            else if (request.FilterType == VideoFilterTypes.ByViewsInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.ViewCount);
            }
            else if (request.FilterType == VideoFilterTypes.ByCommentsDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.CommentCount);
            }
            else if (request.FilterType == VideoFilterTypes.ByCommentsInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.CommentCount);
            }

            var pageParameters = request.PageParameters;

            var videos = (await repository.GetAsync(filter: filterCondition,
                                                   includeProperties: $"{nameof(Entities.Youtube.Video.Comments)},{nameof(Entities.Youtube.Video.Channel)}",
                                                   orderBy:orderByExpression))
                                          .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                          .Take(pageParameters.PageSize).ToList();

            var mappedVideos = mapper.Map<ICollection<SimpleVideoDTO>>(videos);
            foreach (var video in mappedVideos)
            {
                video.IsLoaded = true;
            }

            return mappedVideos;
        }
        #endregion

        #region get-load
        public async Task<VideosWithoutLoadingResponse> GetVideosWithoutLoadingByName(string name, string? nextPageToken, string? channelId, string filterType)
        {
            SearchResource.ListRequest.OrderEnum? orderType;
            if (filterType == LoadFilterOptions.Date)
            {
                orderType = SearchResource.ListRequest.OrderEnum.Date;
            }
            else if (filterType == LoadFilterOptions.Rating)
            {
                orderType = SearchResource.ListRequest.OrderEnum.ViewCount;
            }

            List<string> videoIds;
            string newNextPageToken = null;

            if (channelId!=null && (name==null || name == ""))
            {
                var searchListRequest = youtubeService.PlaylistItems.List("snippet");

                var channelStr = "UU"+channelId.Remove(0, 2);

                searchListRequest.PlaylistId = channelStr;
                searchListRequest.MaxResults = 100;

                if (nextPageToken != null && nextPageToken!="") searchListRequest.PageToken = nextPageToken;

                var searchListResponse = searchListRequest.Execute();

                if (searchListResponse.Items.Count() == 50) newNextPageToken = searchListResponse.NextPageToken;

                videoIds = searchListResponse.Items.Select(item => item.Snippet.ResourceId.VideoId).ToList();
            }
            else
            {
                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = name;

                if (channelId != null && channelId != "") searchListRequest.ChannelId = channelId;
                searchListRequest.MaxResults = 100;

                if (nextPageToken != null && nextPageToken != "") searchListRequest.PageToken = nextPageToken;

                var searchListResponse = searchListRequest.Execute();    

                if (searchListResponse.Items.Count() == 50) newNextPageToken = searchListResponse.NextPageToken;

                videoIds = searchListResponse.Items.Select(item => item.Id.VideoId).ToList();
            }
            
            if (videoIds == null || videoIds.Count() == 0)
            {
                return new VideosWithoutLoadingResponse()
                {
                    Videos = new List<SimpleVideoDTO>(),
                    NextPageToken = null
                };
            }

            

            // Create the videos request to retrieve video statistics and tags
            var videosRequest = youtubeService.Videos.List("snippet,statistics");

            videosRequest.MaxResults = 100;
            // Set the video IDs to load statistics and tags for the specific videos
            videosRequest.Id = string.Join(",", videoIds);

            // Execute the request and return the list of videos
            var videosResponse = videosRequest.Execute();

            var videos = videosResponse.Items;

            List<SimpleVideoDTO> mappedVideos = new List<SimpleVideoDTO>(videoIds.Count());

            foreach (var video in videos)
            {
                var origVideo = (await repository.GetAsync(c => c.Id == video.Id, includeProperties: $"{nameof(Entities.Youtube.Video.Channel)},{nameof(Entities.Youtube.Video.Comments)}")).FirstOrDefault();

                if (origVideo != null)
                {
                    var mappedVideo = mapper.Map<SimpleVideoDTO>(origVideo);
                    mappedVideo.IsLoaded = true;

                    mappedVideos.Add(mappedVideo);
                    continue;
                }


                var newVideo = new Entities.Youtube.Video()
                {
                    Id = video.Id,
                    ETag = video.ETag,
                    CategoryId = video.Snippet.CategoryId,
                    ChannelId = video.Snippet.ChannelId,
                    ChannelTitle = video.Snippet.ChannelTitle,
                    ViewCount = (long)video.Statistics.ViewCount,
                    Title = video.Snippet.Title,
                    Description = video.Snippet.Description,
                    LiveBroadcaseContent = video.Snippet.LiveBroadcastContent,
                    DefaultLanguage = video.Snippet.DefaultLanguage,
                    DefaultAudioLanguage = video.Snippet.DefaultAudioLanguage,
                    PublishedAtRaw = video.Snippet.PublishedAtRaw,
                    VideoImageUrl = video.Snippet.Thumbnails.Medium.Url
                };

                if (video.Statistics.LikeCount != null)
                {
                    newVideo.LikeCount = (int)video.Statistics.LikeCount;
                }

                if (video.Statistics.CommentCount != null)
                {
                    newVideo.CommentCount = (int)video.Statistics.CommentCount;
                }

                try
                {
                    newVideo.PublishedAtDateTimeOffset = (DateTimeOffset)video.Snippet.PublishedAtDateTimeOffset;
                }
                catch { }

                try
                {
                    newVideo.PublishedAt = (DateTime)video.Snippet.PublishedAt;
                }
                catch { }

                mappedVideos.Add(mapper.Map<SimpleVideoDTO>(newVideo));
            }

            return new VideosWithoutLoadingResponse()
            {
                Videos = mappedVideos,
                NextPageToken = newNextPageToken
            };
        }
        #endregion
    }
}

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
using System.Linq.Expressions;
using Domain.DTOs.YoutubeDTOs.Responses;
using Hangfire;
using Domain.Interfaces.Tasks;
using Domain.Interfaces.Customers;
using Domain.Resources.Localization.Tasks;
using Domain.Interfaces.Quotas;
using System.Net;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Interfaces.Other;
using Domain.Resources.Types.DataSources.Youtube;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class YoutubeVideosService : IYoutubeVideoService
    {
        private const int loadVideoQutasCount = 10;

        private readonly IRepository<Entities.DataSources.Youtube.YoutubeVideo> _repository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;
        private readonly IBackgroundJobClient _backgroundJobClient;

        private readonly YouTubeService _youtubeService;
        private readonly IPrivateYoutubeChannelsService _privateChannelService;
        private readonly IYoutubeChannelsService _youtubeChannelService;
        private readonly IMapper _mapper;
        private readonly IMyTasksService _tasksService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public YoutubeVideosService(IRepository<Entities.DataSources.Youtube.YoutubeVideo> repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IPrivateYoutubeChannelsService privateChannelService,
                                     IYoutubeChannelsService youtubeChannelService,
                                     IMapper mapper,
                                     IMyTasksService tasksService,
                                     IBackgroundJobClient backgroundJobClient,
                                     IUserService userService,
                                     IStringLocalizer<TaskTitles> tasksLocalizer,
                                     IQuotasControllerService quotasControllerService)
        {
            _repository = repository;
            _localizer = localizer;
            _privateChannelService = privateChannelService;
            _youtubeChannelService = youtubeChannelService;
            _mapper = mapper;
            _tasksService = tasksService;
            _userService = userService;
            _tasksLocalizer = tasksLocalizer;
            _quotasControllerService = quotasControllerService;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = youtubeOptions["ApiKey"],
                ApplicationName = youtubeOptions["ApplicationName"]
            });
            _backgroundJobClient = backgroundJobClient;
        }

        #region load
        public async Task LoadFromChannel(YoutubeLoadOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingVideosFromYoutube]);

            _backgroundJobClient.Enqueue(() => LoadFromChannelBackgroundJob(options, userId, taskId));
        }
        public async Task LoadFromChannelBackgroundJob(YoutubeLoadOptions options, string userId, int taskId)
        {
            string channelId = options.ParentId;

            bool isNewChannel = false;
            if (await _privateChannelService.GetById(channelId) == null)
            {
                await _youtubeChannelService.LoadById(channelId);
                isNewChannel = true;
            }

            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;
            var logs = Guid.NewGuid().ToString();
            try
            {
                var nextPageToken = "";

                var channel = await _privateChannelService.GetById(channelId);

                for (int i = 0; i < options.MaxLoad;)
                {
                    var searchRequest = _youtubeService.PlaylistItems.List("snippet");

                    var searchListRequest = _youtubeService.Search.List("snippet");
                    searchListRequest.ChannelId = channelId;
                    searchListRequest.Type = "video";
                    searchListRequest.MaxResults = 100;

                    if (options.DateFrom != null) searchListRequest.PublishedAfter = options.DateFrom;
                    if (options.DateTo != null) searchListRequest.PublishedBefore = options.DateTo;

                    if (nextPageToken != null && nextPageToken != "") searchListRequest.PageToken = nextPageToken;

                    var searchListResponse = searchListRequest.Execute();
                    nextPageToken = searchListResponse.NextPageToken;

                    var videoIds = searchListResponse.Items.Select(item => item.Id.VideoId).ToList();

                    // Create the videos request to retrieve video statistics and tags
                    var videosRequest = _youtubeService.Videos.List("snippet,statistics");

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
                                continue;
                            }
                        }

                        if (!isNewChannel)
                        {
                            if (await _repository.FindAsync(video.Id) != null)
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

                        var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, loadVideoQutasCount, logs);

                        if (!quotasResult)
                        {
                            await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                            await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                            return;
                        }

                        var newVideo = new Entities.DataSources.Youtube.YoutubeVideo()
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

                        newVideo.LoaderId = userId;

                        if (video.Statistics.LikeCount != null)
                        {
                            newVideo.LikeCount = (int)video.Statistics.LikeCount;
                        }

                        if (video.Statistics.CommentCount != null)
                        {
                            newVideo.CommentCount = (int)video.Statistics.CommentCount;
                        }
                        await _repository.AddAsync(newVideo);
                        i++;

                        if (channel != null) channel.LoadedVideoCount++;

                        float plusPercent = 100f / options.MaxLoad;
                        percent += plusPercent;

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
        public async Task LoadById(string id)
        {
            if (await _repository.FindAsync(id) != null) throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeVideoAlreadyLoaded], HttpStatusCode.Conflict);

            var videosRequest = _youtubeService.Videos.List("snippet,statistics");

            // Set the channelId to load the specific channel
            videosRequest.Id = id;

            var response = new VideoListResponse();
            try
            {
                response = videosRequest.Execute();
            }
            catch
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeVideolLoadingError], HttpStatusCode.BadRequest);
            }


            if (response.Items == null || response.Items.Count() == 0) throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeVideoDoesNotExist], HttpStatusCode.NotFound);

            var video = response.Items[0];

            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, loadVideoQutasCount, Guid.NewGuid().ToString());

            if (!quotasResult)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }

            try
            {
                var newVideo = new Entities.DataSources.Youtube.YoutubeVideo()
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
                newVideo.LoaderId = userId;

                if (video.Statistics.LikeCount != null)
                {
                    newVideo.LikeCount = (int)video.Statistics.LikeCount;
                }

                if (video.Statistics.CommentCount != null)
                {
                    newVideo.CommentCount = (int)video.Statistics.CommentCount;
                }
                var channel = await _privateChannelService.GetById(video.Snippet.ChannelId);

                if (channel != null)
                {
                    channel.LoadedVideoCount += 1;
                }

                await _repository.AddAsync(newVideo);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeVideoAddingError], HttpStatusCode.BadRequest);
            }
        }
        public async Task LoadManyByIds(ICollection<string> ids)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var logs = Guid.NewGuid().ToString();
            while (ids.Count() > 0)
            {
                // Create the videos request to retrieve video statistics and tags
                var videosRequest = _youtubeService.Videos.List("snippet,statistics");

                videosRequest.MaxResults = 100;
                // Set the video IDs to load statistics and tags for the specific videos
                videosRequest.Id = string.Join(",", ids);

                // Execute the request and return the list of videos
                var videosResponse = videosRequest.Execute();

                var videos = videosResponse.Items;

                foreach (var video in videos)
                {
                    ids.Remove(video.Id);

                    if (await _repository.FindAsync(video.Id) != null) continue;

                    var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, loadVideoQutasCount, logs);

                    if (!quotasResult)
                    {
                        throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                    }

                    var newVideo = new Entities.DataSources.Youtube.YoutubeVideo()
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
                    newVideo.LoaderId = userId;

                    if (video.Statistics.LikeCount != null)
                    {
                        newVideo.LikeCount = (int)video.Statistics.LikeCount;
                    }

                    if (video.Statistics.CommentCount != null)
                    {
                        newVideo.CommentCount = (int)video.Statistics.CommentCount;
                    }

                    var channel = await _privateChannelService.GetById(video.Snippet.ChannelId);
                    if (channel != null)
                    {
                        channel.LoadedVideoCount += 1;
                    }

                    await _repository.AddAsync(newVideo);
                }

                await _repository.SaveChangesAsync();
            }
        }
        #endregion

        #region get
        public async Task<SimpleYoutubeVideoDTO> GetLoadedById(string id)
        {
            var video = (await _repository.GetAsync(c => c.Id == id)).FirstOrDefault();

            if (video == null) throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeVideoNotFound], HttpStatusCode.NotFound);

            var mappedVideo = _mapper.Map<SimpleYoutubeVideoDTO>(video);
            mappedVideo.IsLoaded = true;

            return mappedVideo;
        }
        public async Task<ICollection<SimpleYoutubeVideoDTO>> GetLoadedCollection(GetYoutubeVideosRequest request)
        {
            if (request.FilterStr != null && request.FilterStr != "")
            {
                try
                {
                    var video = await GetLoadedById(request.FilterStr);

                    return new List<SimpleYoutubeVideoDTO>() { video };
                }
                catch { }
            }
            Expression<Func<Entities.DataSources.Youtube.YoutubeVideo, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.Title.Contains(request.FilterStr);
            if (request.ChannelId != null && request.ChannelId != "")
            {
                var channel = await _youtubeChannelService.GetLoadedById(request.ChannelId);

                if (channel != null)
                {
                    filterCondition = e => e.ChannelId == request.ChannelId;
                }
            }


            Func<IQueryable<Entities.DataSources.Youtube.YoutubeVideo>, IOrderedQueryable<Entities.DataSources.Youtube.YoutubeVideo>> orderByExpression = q =>
                q.OrderByDescending(e => e.PublishedAtDateTimeOffset);

            if (request.FilterType == VideoFilterTypes.ByTimeDesc)
            {
                orderByExpression = q =>
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

            var videos = await _repository.GetAsync(filter: filterCondition,
                                                   orderBy: orderByExpression,
                                                   pageParameters: pageParameters);

            var mappedVideos = _mapper.Map<ICollection<SimpleYoutubeVideoDTO>>(videos);
            foreach (var video in mappedVideos)
            {
                video.IsLoaded = true;
            }

            return mappedVideos;
        }
        #endregion

        #region get-load
        public async Task<YoutubeVideosWithoutLoadingResponse> GetCollectionWithoutLoadingByName(string name, string? nextPageToken, string? channelId, string filterType)
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

            if (channelId != null && (name == null || name == ""))
            {
                var searchListRequest = _youtubeService.PlaylistItems.List("snippet");

                var channelStr = "UU" + channelId.Remove(0, 2);

                searchListRequest.PlaylistId = channelStr;
                searchListRequest.MaxResults = 100;

                if (nextPageToken != null && nextPageToken != "") searchListRequest.PageToken = nextPageToken;

                var searchListResponse = searchListRequest.Execute();

                if (searchListResponse.Items.Count() == 50) newNextPageToken = searchListResponse.NextPageToken;

                videoIds = searchListResponse.Items.Select(item => item.Snippet.ResourceId.VideoId).ToList();
            }
            else
            {
                var searchListRequest = _youtubeService.Search.List("snippet");
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
                return new YoutubeVideosWithoutLoadingResponse()
                {
                    Videos = new List<SimpleYoutubeVideoDTO>(),
                    NextPageToken = null
                };
            }



            // Create the videos request to retrieve video statistics and tags
            var videosRequest = _youtubeService.Videos.List("snippet,statistics");

            videosRequest.MaxResults = 100;
            // Set the video IDs to load statistics and tags for the specific videos
            videosRequest.Id = string.Join(",", videoIds);

            // Execute the request and return the list of videos
            var videosResponse = videosRequest.Execute();

            var videos = videosResponse.Items;

            List<SimpleYoutubeVideoDTO> mappedVideos = new List<SimpleYoutubeVideoDTO>(videoIds.Count());

            foreach (var video in videos)
            {
                var origVideo = (await _repository.GetAsync(c => c.Id == video.Id)).FirstOrDefault();

                if (origVideo != null)
                {
                    var mappedVideo = _mapper.Map<SimpleYoutubeVideoDTO>(origVideo);
                    mappedVideo.IsLoaded = true;

                    mappedVideos.Add(mappedVideo);
                    continue;
                }


                var newVideo = new Entities.DataSources.Youtube.YoutubeVideo()
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

                mappedVideos.Add(_mapper.Map<SimpleYoutubeVideoDTO>(newVideo));
            }

            return new YoutubeVideosWithoutLoadingResponse()
            {
                Videos = mappedVideos,
                NextPageToken = newNextPageToken
            };
        }
        #endregion
    }
}

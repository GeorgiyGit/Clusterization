using AutoMapper;
using Domain.DTOs.ExternalData;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Youtube;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Domain.Services.Youtube
{
    public class YoutubeChannelsService : IYoutubeChannelsService
    {
        private const int loadChannelQutasCount = 50;
        private readonly IRepository<Entities.Youtube.Channel> repository;
        private readonly YouTubeService youtubeService;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public YoutubeChannelsService(IRepository<Entities.Youtube.Channel> repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IMapper mapper,
                                     IUserService userService,
                                     IQuotasControllerService quotasControllerService)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;
            _userService = userService;
            _quotasControllerService = quotasControllerService;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = youtubeOptions["ApiKey"],
                ApplicationName = youtubeOptions["ApplicationName"]
            });
        }

        #region load
        public async Task LoadById(string id)
        {
            if (await repository.FindAsync(id) != null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeChannelAlreadyLoaded], System.Net.HttpStatusCode.Conflict);

            var channelsRequest = youtubeService.Channels.List("snippet,statistics");

            // Set the channelId to load the specific channel
            channelsRequest.Id = id;

            var response = new ChannelListResponse();
            try
            {
                response = channelsRequest.Execute();
            }
            catch 
            {
                throw new HttpException(localizer[ErrorMessagePatterns.YoutubeChannelLoadingError], System.Net.HttpStatusCode.BadRequest);
            }
            

            if (response.Items == null || response.Items.Count()==0) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeChannelDoesNotExist], System.Net.HttpStatusCode.NotFound);

            var channel = response.Items[0];

            var customerId = await _userService.GetCurrentUserId();

            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(customerId, QuotasTypes.Youtube, loadChannelQutasCount, Guid.NewGuid().ToString());

            if (!quotasResult)
            {
                throw new HttpException(localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }

            try
            {
                var newChannel = new Entities.Youtube.Channel()
                {
                    Id = channel.Id,
                    ETag = channel.ETag,
                    Country = channel.Snippet.Country,
                    CustomUrl = channel.Snippet.CustomUrl,
                    Title = channel.Snippet.Title,
                    Description = channel.Snippet.Description,
                    PublishedAtRaw = channel.Snippet.PublishedAtRaw,
                    ChannelImageUrl = channel.Snippet.Thumbnails.Medium.Url,
                    SubscriberCount = (long)channel.Statistics.SubscriberCount,
                    VideoCount = (int)channel.Statistics.VideoCount,
                    ViewCount = (long)channel.Statistics.ViewCount,
                    LoadedDate = DateTime.UtcNow
                };

                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);
                newChannel.LoaderId = userId;

                try
                {
                    newChannel.PublishedAtDateTimeOffset = (DateTimeOffset)channel.Snippet.PublishedAtDateTimeOffset;
                }
                catch { }

                try
                {
                    newChannel.PublishedAt = (DateTime)channel.Snippet.PublishedAt;
                }
                catch { }

                await repository.AddAsync(newChannel);
                await repository.SaveChangesAsync();
            }
            catch
            {
                throw new HttpException(localizer[ErrorMessagePatterns.YoutubeChannelAddingError], System.Net.HttpStatusCode.BadRequest);
            }
        }
        public async Task LoadManyByIds(ICollection<string> ids)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(localizer[ErrorMessagePatterns.UserNotAuthorized], System.Net.HttpStatusCode.BadRequest);

            var logs = Guid.NewGuid().ToString();
            while (ids.Count() > 0)
            {
                // Create the videos request to retrieve video statistics and tags
                var chanelsRequest = youtubeService.Channels.List("snippet,statistics");

                chanelsRequest.MaxResults = 100;
                // Set the video IDs to load statistics and tags for the specific videos
                chanelsRequest.Id = string.Join(",", ids);

                // Execute the request and return the list of videos
                var chanelsResponse = chanelsRequest.Execute();

                var chanels = chanelsResponse.Items;

                foreach (var channel in chanels)
                {
                    ids.Remove(channel.Id);

                    if ((await repository.FindAsync(channel.Id)) != null) continue;

                    var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Youtube, loadChannelQutasCount, logs);

                    if (!quotasResult)
                    {
                        throw new HttpException(localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                    }

                    var newChannel = new Entities.Youtube.Channel()
                    {
                        Id = channel.Id,
                        ETag = channel.ETag,
                        Country = channel.Snippet.Country,
                        CustomUrl = channel.Snippet.CustomUrl,
                        Title = channel.Snippet.Title,
                        Description = channel.Snippet.Description,
                        PublishedAtRaw = channel.Snippet.PublishedAtRaw,
                        ChannelImageUrl = channel.Snippet.Thumbnails.Medium.Url,
                        SubscriberCount = (long)channel.Statistics.SubscriberCount,
                        VideoCount = (int)channel.Statistics.VideoCount,
                        ViewCount = (long)channel.Statistics.ViewCount,
                        LoadedDate = DateTime.UtcNow
                    };

                    newChannel.LoaderId = userId;

                    try
                    {
                        newChannel.PublishedAtDateTimeOffset = (DateTimeOffset)channel.Snippet.PublishedAtDateTimeOffset;
                    }
                    catch { }

                    try
                    {
                        newChannel.PublishedAt = (DateTime)channel.Snippet.PublishedAt;
                    }
                    catch { }

                    await repository.AddAsync(newChannel);
                }

                await repository.SaveChangesAsync();
            }
        }
        #endregion

        #region get
        public async Task<SimpleChannelDTO> GetLoadedById(string id)
        {
            var channel = (await repository.GetAsync(c => c.Id == id, pageParameters:null)).FirstOrDefault();

            if (channel == null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeChannelNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedChannel = mapper.Map<SimpleChannelDTO>(channel);
            mappedChannel.IsLoaded = true;

            return mappedChannel;
        }
        public async Task<ICollection<SimpleChannelDTO>> GetLoadedCollection(GetChannelsRequest request)
        {
            if (request.FilterStr != null && request.FilterStr != "")
            {
                try
                {
                    var channel = await GetLoadedById(request.FilterStr);

                    return new List<SimpleChannelDTO>() { channel };
                }
                catch { }
            }
            Expression<Func<Entities.Youtube.Channel, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.Title.Contains(request.FilterStr);

            Func<IQueryable<Entities.Youtube.Channel>, IOrderedQueryable<Entities.Youtube.Channel>> orderByExpression = q =>
                q.OrderByDescending(e => e.PublishedAtDateTimeOffset);

            if (request.FilterType == ChannelFilterTypes.ByTimeDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.PublishedAtDateTimeOffset);
            }
            else if (request.FilterType == ChannelFilterTypes.ByTimeInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.PublishedAtDateTimeOffset);
            }
            else if (request.FilterType == ChannelFilterTypes.ByVideoCountDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.VideoCount);
            }
            else if (request.FilterType == ChannelFilterTypes.ByVideoCountInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.VideoCount);
            }
            else if (request.FilterType == ChannelFilterTypes.BySubscribersDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.SubscriberCount);
            }
            else if (request.FilterType == ChannelFilterTypes.BySubscribersInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.SubscriberCount);
            }

            var pageParameters = request.PageParameters;

            var channels = (await repository.GetAsync(filter: filterCondition,
                                                      orderBy: orderByExpression,
                                                      pageParameters: pageParameters));


            var mappedChannels = mapper.Map<ICollection<SimpleChannelDTO>>(channels);
            foreach (var channel in mappedChannels)
            {
                channel.IsLoaded = true;
            }
            return mappedChannels;
        }
        #endregion

        #region get-load
        public async Task<ChannelsWithoutLoadingResponse> GetCollectionWithoutLoadingByName(string name, string? nextPageToken, string filterType)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = name;
            searchListRequest.Type = "channel";
            searchListRequest.MaxResults = 100;

            if (filterType == LoadFilterOptions.Date)
            {
                searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            }
            else if (filterType == LoadFilterOptions.Rating)
            {
                searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            }


            if (nextPageToken != null) searchListRequest.PageToken = nextPageToken;

            var searchListResponse = searchListRequest.Execute();

            string newNextPageToken = null;

            if (searchListResponse.Items.Count() == 50) newNextPageToken = searchListResponse.NextPageToken;

            if (searchListResponse.Items == null || searchListResponse.Items.Count() == 0)
            {
                return new ChannelsWithoutLoadingResponse()
                {
                    Channels = new List<SimpleChannelDTO>(),
                    NextPageToken = null
                };
            }

            var channelIds = searchListResponse.Items.Where(item => item.Id.Kind == "youtube#channel").Select(item => item.Id.ChannelId).ToList();

            if (channelIds.Count() == 0)
            {
                return new ChannelsWithoutLoadingResponse()
                {
                    Channels = new List<SimpleChannelDTO>(),
                    NextPageToken = null
                };
            }

            // Create the videos request to retrieve video statistics and tags
            var channelsRequest = youtubeService.Channels.List("snippet,statistics");

            channelsRequest.MaxResults = 100;
            // Set the video IDs to load statistics and tags for the specific videos
            channelsRequest.Id = string.Join(",", channelIds);

            // Execute the request and return the list of videos
            var channelsResponse = channelsRequest.Execute();

            var channels = channelsResponse.Items;

            List<SimpleChannelDTO> mappedChannels = new List<SimpleChannelDTO>(channelIds.Count());

            foreach (var channel in channels)
            {
                var origChannel = (await repository.GetAsync(c => c.Id == channel.Id, pageParameters: null)).FirstOrDefault();

                if (origChannel != null)
                {
                    var mappedChannel = mapper.Map<SimpleChannelDTO>(origChannel);
                    mappedChannel.IsLoaded = true;

                    mappedChannels.Add(mappedChannel);
                    continue;
                }

                var newChannel = new Entities.Youtube.Channel()
                {
                    Id = channel.Id,
                    ETag = channel.ETag,
                    Country = channel.Snippet.Country,
                    CustomUrl = channel.Snippet.CustomUrl,
                    Title = channel.Snippet.Title,
                    Description = channel.Snippet.Description,            
                    PublishedAtRaw = channel.Snippet.PublishedAtRaw,
                    ChannelImageUrl = channel.Snippet.Thumbnails.Medium.Url,
                    SubscriberCount = (long)channel.Statistics.SubscriberCount,
                    VideoCount = (int)channel.Statistics.VideoCount,
                    ViewCount = (long)channel.Statistics.ViewCount,
                };

                try
                {
                    newChannel.PublishedAtDateTimeOffset = (DateTimeOffset)channel.Snippet.PublishedAtDateTimeOffset;
                }
                catch { }

                try
                {
                    newChannel.PublishedAt = (DateTime)channel.Snippet.PublishedAt;
                }
                catch { }

                mappedChannels.Add(mapper.Map<SimpleChannelDTO>(newChannel));
            }

            if (filterType == LoadFilterOptions.Date)
            {
                mappedChannels = mappedChannels.OrderBy(e => e.PublishedAtDateTimeOffset).ToList();
            }
            else if (filterType == LoadFilterOptions.Rating)
            {
                mappedChannels = mappedChannels.OrderBy(e => e.ViewCount).ToList();
            }

            return new ChannelsWithoutLoadingResponse()
            {
                Channels = mappedChannels,
                NextPageToken = newNextPageToken
            };
        }
        #endregion
    }
}

using AutoMapper;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Youtube;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
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
using System.Threading.Tasks;

namespace Domain.Services.Youtube
{
    public class YoutubeChannelService : IYoutubeChannelService
    {
        private readonly IRepository<Entities.Youtube.Channel> repository;
        private readonly YouTubeService youtubeService;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IMapper mapper;
        public YoutubeChannelService(IRepository<Entities.Youtube.Channel> repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     IConfiguration configuration,
                                     IMapper mapper)
        {
            this.repository = repository;
            this.localizer = localizer;
            this.mapper = mapper;

            var youtubeOptions = configuration.GetSection("YoutubeOptions");

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = youtubeOptions["ApiKey"],
                ApplicationName = youtubeOptions["ApplicationName"]
            });
        }

        #region load
        public async Task LoadChannel(string id)
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
                    ChannelImageUrl = channel.Snippet.Thumbnails.Default__.Url,
                    SubscriberCount = (long)channel.Statistics.SubscriberCount,
                    VideoCount = (int)channel.Statistics.VideoCount,
                    ViewCount = (long)channel.Statistics.ViewCount,
                    LoadedDate = DateTime.UtcNow
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
            while(ids.Count()>0)
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
                    var newChannel = new Entities.Youtube.Channel()
                    {
                        Id = channel.Id,
                        ETag = channel.ETag,
                        Country = channel.Snippet.Country,
                        CustomUrl = channel.Snippet.CustomUrl,
                        Title = channel.Snippet.Title,
                        Description = channel.Snippet.Description,
                        PublishedAtRaw = channel.Snippet.PublishedAtRaw,
                        ChannelImageUrl = channel.Snippet.Thumbnails.Default__.Url,
                        SubscriberCount = (long)channel.Statistics.SubscriberCount,
                        VideoCount = (int)channel.Statistics.VideoCount,
                        ViewCount = (long)channel.Statistics.ViewCount,
                        LoadedDate = DateTime.UtcNow
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

                    await repository.AddAsync(newChannel);
                }

                await repository.SaveChangesAsync();
            }
        }
        #endregion

        #region get
        public async Task<SimpleChannelDTO> GetLoadedChannelById(string id)
        {
            var channel = (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Entities.Youtube.Channel.Comments)},{nameof(Entities.Youtube.Channel.Videos)}")).FirstOrDefault();

            if (channel == null) throw new HttpException(localizer[ErrorMessagePatterns.YoutubeChannelNotFound], System.Net.HttpStatusCode.NotFound);

            var mappedChannel = mapper.Map<SimpleChannelDTO>(channel);
            mappedChannel.IsLoaded = true;

            return mappedChannel;
        }
        public async Task<ICollection<SimpleChannelDTO>> GetLoadedChannels(GetChannelsRequest request)
        {
            if (request.FilterStr != null && request.FilterStr != "")
            {
                try
                {
                    var channel = await GetLoadedChannelById(request.FilterStr);

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
                                                   includeProperties: $"{nameof(Entities.Youtube.Channel.Comments)},{nameof(Entities.Youtube.Channel.Videos)}",
                                                   orderBy: orderByExpression))
                                            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                            .Take(pageParameters.PageSize).ToList();


            var mappedChannels = mapper.Map<ICollection<SimpleChannelDTO>>(channels);
            foreach (var channel in mappedChannels)
            {
                channel.IsLoaded = true;
            }
            return mappedChannels;
        }
        #endregion

        #region get-load
        public async Task<ChannelsWithoutLoadingResponse> GetChannelsWithoutLoadingByName(string name, string? nextPageToken, string filterType)
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
                var origChannel = (await repository.GetAsync(c => c.Id == channel.Id, includeProperties: $"{nameof(Entities.Youtube.Channel.Videos)},{nameof(Entities.Youtube.Channel.Comments)}")).FirstOrDefault();

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
                    ChannelImageUrl = channel.Snippet.Thumbnails.Default__.Url,
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

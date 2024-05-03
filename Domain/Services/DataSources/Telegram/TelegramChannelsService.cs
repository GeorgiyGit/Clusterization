using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Responses;
using Domain.Exceptions;
using Domain.Interfaces.DataSources.Telegram;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.DataSources.Telegram;
using AutoMapper;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Quotas;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using TL;
using WTelegram;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.Resources.Types.DataSources.Youtube;
using System.Linq.Expressions;
using Domain.Resources.Types.DataSources.Telegram;

namespace Domain.Services.DataSources.Telegram
{
    public class TelegramChannelsService:ITelegramChannelsService
    {
        private const int loadChannelQutasCount = 50;

        private readonly IRepository<TelegramChannel> _repository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        private readonly WTelegramService _wTelegramService;
        public TelegramChannelsService(IRepository<TelegramChannel> repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     WTelegramService wTelegramService,
                                     IMapper mapper,
                                     IUserService userService,
                                     IQuotasControllerService quotasControllerService)
        {
            _repository = repository;
            _localizer = localizer;
            _mapper = mapper;
            _userService = userService;
            _quotasControllerService = quotasControllerService;
            _wTelegramService = wTelegramService;
        }
        public async Task LoadByUsername(string username)
        {
            if ((await _repository.GetAsync(e=>e.Username==username)).Any()) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelAlreadyLoaded], HttpStatusCode.Conflict);


            var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(username);
            if (resolveRes == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);

            var response = await _wTelegramService.Client.Channels_GetFullChannel(new InputChannel(resolveRes.Channel.ID, resolveRes.Channel.access_hash));

            if(response==null || response.chats==null || response.chats.Count == 0)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);
            }

            var channel = response.full_chat;

            var customerId = await _userService.GetCurrentUserId();

            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(customerId, QuotasTypes.Youtube, loadChannelQutasCount, Guid.NewGuid().ToString());

            if (!quotasResult)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }

            try
            {
                var newChannel = new TelegramChannel()
                {
                    TelegramID = channel.ID,
                    About = channel.About,
                    Username = username,
                    ParticipantsCount = channel.ParticipantsCount,
                };

                if (channel.ChatPhoto != null)
                {
                    newChannel.PhotoId = channel.ChatPhoto.ID;
                }
                var smallChat = response.chats[channel.ID];
                if (smallChat != null)
                {
                    newChannel.Title = smallChat.Title;
                    newChannel.IsActive = smallChat.IsActive;

                    if (smallChat is Chat)
                    {
                        newChannel.Date = (smallChat as Chat).date;
                    }
                    if (smallChat is TL.Channel)
                    {
                        newChannel.Date = (smallChat as TL.Channel).date;
                    }
                }

                var userId = await _userService.GetCurrentUserId();
                if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);
                newChannel.LoaderId = userId;

                await _repository.AddAsync(newChannel);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.YoutubeChannelAddingError], HttpStatusCode.BadRequest);
            }
        }
        public async Task LoadManyByUsernames(ICollection<string> userNames)
        {
            foreach(var userName in userNames)
            {
                await LoadByUsername(userName);
            }
        }

        public async Task<FullTelegramChannelDTO> GetLoadedById(long id)
        {
            var channel = (await _repository.GetAsync(c => c.Id == id)).FirstOrDefault();

            if (channel == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<FullTelegramChannelDTO>(channel);
        }
        public async Task<ICollection<SimpleTelegramChannelDTO>> GetLoadedCollection(GetTelegramChannelsRequest request)
        {
            Expression<Func<TelegramChannel, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.Title.Contains(request.FilterStr);

            Func<IQueryable<TelegramChannel>, IOrderedQueryable<TelegramChannel>> orderByExpression = q =>
                q.OrderByDescending(e => e.LoadedDate);

            if (request.FilterType == TelegramChannelFilterTypes.ByTimeDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.LoadedDate);
            }
            else if (request.FilterType == TelegramChannelFilterTypes.ByTimeInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.LoadedDate);
            }
            else if (request.FilterType == TelegramChannelFilterTypes.ByLoadedMessageCountDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.TelegramMessagesCount);
            }
            else if (request.FilterType == TelegramChannelFilterTypes.ByLoadedMessageCountInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.TelegramMessagesCount);
            }
            else if (request.FilterType == TelegramChannelFilterTypes.ByParticipantsDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.ParticipantsCount);
            }
            else if (request.FilterType == TelegramChannelFilterTypes.ByParticipantsInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.ParticipantsCount);
            }

            var pageParameters = request.PageParameters;

            var channels = await _repository.GetAsync(filter: filterCondition,
                                                      orderBy: orderByExpression,
                                                      pageParameters: pageParameters);


            var mappedChannels = _mapper.Map<ICollection<SimpleTelegramChannelDTO>>(channels);
            return mappedChannels;
        }

        public async Task<ICollection<SimpleTelegramChannelDTO>> GetCollectionWithoutLoadingByName(string name)
        {
            var response = await _wTelegramService.Client.Contacts_Search(name, 20);

            var filteredResponse = response.chats.Where(e => e.Value.IsChannel).Select(e => e.Value);

            var channels = new List<SimpleTelegramChannelDTO>();
            foreach (var responseChannel in filteredResponse)
            {
                var orig = responseChannel as TL.Channel;

                if (responseChannel.MainUsername == null) continue;

                var newChannel = new SimpleTelegramChannelDTO()
                {
                    Id = -1,
                    IsActive = responseChannel.IsActive,
                    LoadedDate = DateTime.UtcNow,
                    ParticipantsCount = orig.participants_count,
                    TelegramMessagesCount = 0,
                    TelegramRepliesCount = 0,
                    Title = responseChannel.Title,
                    IsLoaded = false,
                    Date = orig.date,
                    Username=responseChannel.MainUsername
                };
                

                if (responseChannel.Photo != null)
                {
                    newChannel.PhotoId = responseChannel.Photo.photo_id;
                }

                channels.Add(newChannel);
            }

            return channels;
        }
    }
}

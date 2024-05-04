using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types;
using Hangfire;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;
using TL;
using Domain.Interfaces.DataSources.Telegram;
using Domain.Entities.DataSources.Telegram;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using Accord.Statistics.Kernels;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Responses;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Requests;
using Domain.Resources.Types.DataSources.Telegram;

namespace Domain.Services.DataSources.Telegram
{
    public class TelegramMessagesService: ITelegramMessagesService
    {
        private const int loadMsgQutasCount = 5;

        private readonly IRepository<TelegramMessage> _repository;
        private readonly IRepository<TelegramChannel> _channelsRepository;
        private readonly IRepository<TelegramReaction> _reactionsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;
        private readonly IBackgroundJobClient _backgroundJobClient;

        private readonly WTelegramService _wTelegramService;
        private readonly IMapper _mapper;
        private readonly IMyTasksService _tasksService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public TelegramMessagesService(IRepository<TelegramMessage> repository,
                                     IStringLocalizer<ErrorMessages> localizer,
                                     WTelegramService wTelegramService,
                                     IMapper mapper,
                                     IMyTasksService tasksService,
                                     IBackgroundJobClient backgroundJobClient,
                                     IUserService userService,
                                     IStringLocalizer<TaskTitles> tasksLocalizer,
                                     IQuotasControllerService quotasControllerService,
                                     IRepository<TelegramChannel> channelsRepository,
                                     IRepository<TelegramReaction> reactionsRepository)
        {
            _repository = repository;
            _channelsRepository = channelsRepository;

            _localizer = localizer;
            _mapper = mapper;
            _tasksService = tasksService;
            _userService = userService;
            _tasksLocalizer = tasksLocalizer;
            _quotasControllerService = quotasControllerService;

            _wTelegramService = wTelegramService;
            _backgroundJobClient = backgroundJobClient;
            _reactionsRepository = reactionsRepository;
        }

        #region load
        public async Task LoadFromChannel(TelegramLoadOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingMessagesFromTelegram]);

            _backgroundJobClient.Enqueue(() => LoadFromChannelBackgroundJob(options, userId, taskId));
        }
        public async Task LoadFromChannelBackgroundJob(TelegramLoadOptions options, string userId, int taskId)
        {
            long channelId = (long)options.ParentId;

            if (await _channelsRepository.FindAsync(channelId) == null)
            {
                await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.TelegramChannelNotFound]);
                return;
            }

            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;
            var logs = Guid.NewGuid().ToString();
            try
            {
                var channel = await _channelsRepository.FindAsync(channelId);

                var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(channel.Username);
                if (resolveRes == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);

                for (int i = 0; i < options.MaxLoad;)
                {
                    Messages_MessagesBase res;
                    if (options.DateTo != null)
                    {
                        res = await _wTelegramService.Client.Messages_GetHistory(new InputPeerChannel(channel.TelegramID, resolveRes.Channel.access_hash), offset_date: (DateTime)options.DateTo, limit: 100, add_offset: i);
                    }
                    else
                    {
                        res = await _wTelegramService.Client.Messages_GetHistory(new InputPeerChannel(channel.TelegramID, resolveRes.Channel.access_hash), limit: 100, add_offset: i);
                    }

                    var messages = res.Messages;

                    if (messages == null || messages.Length == 0) break;

                    foreach (var msg in messages)
                    {
                        if (i >= options.MaxLoad) break;

                        if (!(msg is TL.Message)) continue;

                        var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Telegram, loadMsgQutasCount, logs);

                        if (!quotasResult)
                        {
                            await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                            await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                            return;
                        }

                        await AddMessageToDb(msg, userId, channel.Id);
                        i++;

                        channel.TelegramMessagesCount++;

                        float plusPercent = 100f / options.MaxLoad;
                        percent += plusPercent;

                        await _tasksService.ChangeTaskPercent(taskId, percent);
                    }
                    await _repository.SaveChangesAsync();
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

        private async Task<TelegramMessage?> AddMessageToDb(MessageBase msg, string userId, long channelId)
        {
            var origMsg = msg as TL.Message;

            if (origMsg.message == null || origMsg.message == "") return null;

            var newMsg = new TelegramMessage()
            {
                Date = origMsg.Date,
                EditDate = origMsg.edit_date,
                LoaderId = userId,
                TelegramID = origMsg.ID,
                Message = origMsg.message,
                TelegramChannelId = channelId,
                Views = origMsg.views,
                PostAuthor = origMsg.post_author,
            };

            await _repository.AddAsync(newMsg);

            if (origMsg.reactions != null)
            {
                foreach (var reaction in origMsg.reactions.results)
                {
                    var newReaction = new TelegramReaction()
                    {
                        Count = reaction.count,
                        TelegramMessage = newMsg
                    };
                    if (reaction.reaction != null)
                    {
                        if(reaction.reaction is TL.ReactionEmoji)
                        {
                            newReaction.Emotion = (reaction.reaction as TL.ReactionEmoji).emoticon;
                        }
                        else if (reaction.reaction is TL.ReactionCustomEmoji)
                        {
                            newReaction.DocumentId = (reaction.reaction as TL.ReactionCustomEmoji).document_id;
                            newReaction.IsCustom = true;
                        }
                    }
                    await _reactionsRepository.AddAsync(newReaction);
                }
            }

            await _repository.SaveChangesAsync();

            return newMsg;
        }

        public async Task LoadById(int id, long channelId)
        {
            if (await _repository.FindAsync(id) != null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageAlreadyLoaded], HttpStatusCode.Conflict);

            var channel = (await _channelsRepository.FindAsync(channelId));

            var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(channel.Username);
            if (resolveRes == null || resolveRes.Channel==null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);


            Messages_MessagesBase response;
            try
            {
                response = await _wTelegramService.Client.Channels_GetMessages(new InputChannel(channel.TelegramID, resolveRes.Channel.access_hash), new InputMessageID()
                {
                    id = id
                });
            }
            catch
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageLoadingError], HttpStatusCode.BadRequest);
            }


            if (response.Messages == null || response.Messages.Count() == 0) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageNotFound], HttpStatusCode.NotFound);

            var msg = response.Messages[0];

            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Telegram, loadMsgQutasCount, Guid.NewGuid().ToString());

            if (!quotasResult)
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
            }

            try
            {
                await AddMessageToDb(msg, userId, channel.Id);
                channel.TelegramMessagesCount++;
            }
            catch
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageLoadingError], HttpStatusCode.BadRequest);
            }
        }

        public async Task LoadManyByIds(ICollection<int> ids, long channelId)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var logs = Guid.NewGuid().ToString();

            var channel = (await _channelsRepository.FindAsync(channelId));

            var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(channel.Username);
            if (resolveRes == null || resolveRes.Channel == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);

            var inputList = new List<InputMessageID>();

            foreach(var id in ids)
            {
                inputList.Add(new InputMessageID()
                {
                    id = id
                });
            }

            Messages_MessagesBase response;
            try
            {
                response = await _wTelegramService.Client.Channels_GetMessages(new InputChannel(channel.TelegramID, resolveRes.Channel.access_hash), inputList.ToArray());
            }
            catch
            {
                throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageLoadingError], HttpStatusCode.BadRequest);
            }


            if (response.Messages == null || response.Messages.Count() == 0) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageNotFound], HttpStatusCode.NotFound);

            foreach(var msg in response.Messages)
            {
                if ((await _repository.GetAsync(e => e.TelegramChannelId == channel.Id && e.TelegramID == msg.ID)).Any()) continue;
                var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Telegram, loadMsgQutasCount, logs);

                if (!quotasResult)
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.NotEnoughQuotas], HttpStatusCode.BadRequest);
                }

                try
                {
                    await AddMessageToDb(msg, userId, channel.Id);
                    channel.TelegramMessagesCount++;
                }
                catch
                {
                    throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageLoadingError], HttpStatusCode.BadRequest);
                }
            }
        }
        #endregion

        #region get
        public async Task<FullTelegramMessageDTO> GetLoadedById(long id)
        {
            var msg = (await _repository.GetAsync(c => c.Id == id)).FirstOrDefault();

            if (msg == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramMessageNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<FullTelegramMessageDTO>(msg);
        }
        public async Task<ICollection<SimpleTelegramMessageDTO>> GetLoadedCollection(GetTelegramMessagesRequest request)
        {
            Expression<Func<TelegramMessage, bool>> filterCondition = e => string.IsNullOrEmpty(request.FilterStr) || e.Message.Contains(request.FilterStr);
            if (request.ChannelId != null)
            {
                var channel = await _channelsRepository.FindAsync(request.ChannelId);

                if (channel != null)
                {
                    filterCondition = e => e.TelegramChannelId == request.ChannelId;
                }
            }


            Func<IQueryable<TelegramMessage>, IOrderedQueryable<TelegramMessage>> orderByExpression = q =>
                q.OrderByDescending(e => e.Date);

            if (request.FilterType == TelegramMessageFilterTypes.ByTimeDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.Date);
            }
            else if (request.FilterType == TelegramMessageFilterTypes.ByTimeInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.Date);
            }
            else if (request.FilterType == TelegramMessageFilterTypes.ByViewsDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.Views);
            }
            else if (request.FilterType == TelegramMessageFilterTypes.ByViewsInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.Views);
            }
            else if (request.FilterType == TelegramMessageFilterTypes.ByRepliesDesc)
            {
                orderByExpression = q =>
                        q.OrderByDescending(e => e.TelegramRepliesCount);
            }
            else if (request.FilterType == TelegramMessageFilterTypes.ByRepliesInc)
            {
                orderByExpression = q =>
                        q.OrderBy(e => e.TelegramRepliesCount);
            }

            var pageParameters = request.PageParameters;

            var msgs = await _repository.GetAsync(filter: filterCondition,
                                                  orderBy: orderByExpression,
                                                  pageParameters: pageParameters);

            var mappedMsgs = _mapper.Map<ICollection<SimpleTelegramMessageDTO>>(msgs);

            return mappedMsgs;
        }
        #endregion

        #region get-load
        public async Task<ICollection<SimpleTelegramMessageDTO>> GetCollectionWithoutLoadingByName(GetTelegramMessagesRequest request)
        {
            var channel = await _channelsRepository.FindAsync(request.ChannelId);

            var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(channel.Username);
            if (resolveRes == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);

            var skip = (request.PageParameters.PageNumber - 1) * request.PageParameters.PageSize;
            Messages_MessagesBase res = await _wTelegramService.Client.Messages_GetHistory(new InputPeerChannel(channel.TelegramID, resolveRes.Channel.access_hash),
                                                                                           limit: request.PageParameters.PageSize,
                                                                                           add_offset: skip);

            List<SimpleTelegramMessageDTO> mappedMsgs = new List<SimpleTelegramMessageDTO>(res.Messages.Length);

            foreach (var msg in res.Messages)
            {
                var origOurMsg = (await _repository.GetAsync(e => e.TelegramChannelId == channel.Id && e.TelegramID == msg.ID)).FirstOrDefault();

                if (origOurMsg != null)
                {
                    var mappedMsg = _mapper.Map<SimpleTelegramMessageDTO>(origOurMsg);

                    mappedMsgs.Add(mappedMsg);
                    continue;
                }

                var origMsg = msg as TL.Message;

                if (origMsg.message == null || origMsg.message == "") continue;

                var newMsg = new TelegramMessage()
                {
                    Id=origMsg.ID,
                    Date = origMsg.Date,
                    EditDate = origMsg.edit_date,
                    TelegramID = origMsg.ID,
                    Message = origMsg.message,
                    TelegramChannelId = channel.Id,
                    Views = origMsg.views,
                    PostAuthor = origMsg.post_author,
                };

                var mappedMsg2 = _mapper.Map<SimpleTelegramMessageDTO>(newMsg);
                mappedMsg2.IsLoaded = false;
                mappedMsgs.Add(mappedMsg2);
            }

            return mappedMsgs;
        }
        #endregion
    }
}

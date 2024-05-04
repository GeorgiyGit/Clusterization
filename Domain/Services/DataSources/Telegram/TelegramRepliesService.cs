using AutoMapper;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types.DataSources.Youtube;
using Domain.Resources.Types;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.DataSources.Telegram;
using Domain.Interfaces.DataSources.Telegram;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using TL;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Requests;
using Domain.Extensions;
using static Google.Apis.Requests.BatchRequest;

namespace Domain.Services.DataSources.Telegram
{
    public class TelegramRepliesService:ITelegramRepliesService
    {
        private readonly IRepository<TelegramReply> _repository;
        private readonly IRepository<TelegramMessage> _messagesRepository;
        private readonly IRepository<TelegramChannel> _channelsRepository;
        private readonly IRepository<TelegramReaction> _reactionsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;
        private readonly IBackgroundJobClient _backgroundJobClient;

        private readonly WTelegramService _wTelegramService;
        private readonly ITelegramMessagesService _messagesService;
        private readonly IMapper _mapper;
        private readonly IMyTasksService _tasksService;
        private readonly IUserService _userService;
        private readonly IQuotasControllerService _quotasControllerService;
        public TelegramRepliesService(IRepository<TelegramReply> repository,
                                      WTelegramService wTelegramService,
                                      ITelegramMessagesService messagesService,
                                      IMapper mapper,
                                      IStringLocalizer<ErrorMessages> localizer,
                                      IMyTasksService tasksService,
                                      IBackgroundJobClient backgroundJobClient,
                                      IRepository<TelegramMessage> messagesRepository,
                                      IUserService userService,
                                      IStringLocalizer<TaskTitles> tasksLocalizer,
                                      IQuotasControllerService quotasControllerService,
                                      IRepository<TelegramChannel> channelsRepository,
                                      IRepository<TelegramReaction> reactionsRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _localizer = localizer;
            _tasksService = tasksService;
            _backgroundJobClient = backgroundJobClient;
            _userService = userService;
            _quotasControllerService = quotasControllerService;
            _tasksLocalizer = tasksLocalizer;
            _wTelegramService = wTelegramService;
            _messagesRepository = messagesRepository;
            _messagesService = messagesService;
            _channelsRepository = channelsRepository;
            _reactionsRepository = reactionsRepository;
        }

        #region load
        public async Task LoadFromMessage(TelegramLoadOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingRepliesFromTelegram]);

            _backgroundJobClient.Enqueue(() => LoaRepliesFromMessageBackgroundJob(options, userId, taskId));
        }
        public async Task LoaRepliesFromMessageBackgroundJob(TelegramLoadOptions options, string userId, int taskId)
        {
            long messageId = (long)options.ParentId;

            if (await _messagesRepository.FindAsync(messageId) == null)
            {
                return;
            }

            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;

            var logsId = Guid.NewGuid().ToString();
            try
            {
                var message = await _messagesRepository.FindAsync(messageId);
                var channel = await _channelsRepository.FindAsync(message.TelegramChannelId);

                var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(channel.Username);
                if (resolveRes == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);

                var origMsgResp = await _wTelegramService.Client.Channels_GetMessages(new InputChannel(channel.TelegramID, resolveRes.Channel.access_hash), new InputMessageID()
                {
                    id = message.TelegramID
                });

                if (origMsgResp.Messages == null ||
                    origMsgResp.Messages.Length == 0 ||
                    !(origMsgResp.Messages[0] is Message) ||
                    (origMsgResp.Messages[0] as Message).replies == null ||
                    (origMsgResp.Messages[0] as Message).replies.replies == 0) return;

                int skipCount = 0;
                for (int i = 0; i < options.MaxLoad;)
                {
                    Messages_MessagesBase res;
                    if (options.DateTo != null)
                    {
                        res = await _wTelegramService.Client.Messages_GetReplies(new InputPeerChannel(channel.TelegramID, resolveRes.Channel.access_hash),message.TelegramID, offset_date: (DateTime)options.DateTo, limit: 100, add_offset: skipCount);
                    }
                    else
                    {
                        res = await _wTelegramService.Client.Messages_GetReplies(new InputPeerChannel(channel.TelegramID, resolveRes.Channel.access_hash), message.TelegramID, limit: 100, add_offset: skipCount);
                    }

                    var replies = res.Messages;

                    if (replies == null || replies.Length == 0) break;

                    foreach (var reply in replies)
                    {
                        skipCount++;
                        if (i >= options.MaxLoad) break;

                        if (!(reply is TL.Message)) continue;

                        var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Telegram, 1, logsId);

                        if (!quotasResult)
                        {
                            await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                            await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                            return;
                        }

                        await AddReplyToDb(reply, userId, channel.Id, message.Id);


                        i++;

                        float plusPercent = 100f / options.MaxLoad;
                        percent += plusPercent;


                        if (message != null)
                        {
                            message.TelegramRepliesCount++;

                            if (channel != null)
                            {
                                channel.TelegramRepliesCount++;
                            }
                        }

                        await _tasksService.ChangeTaskPercent(taskId, percent);
                    }

                    await _repository.SaveChangesAsync();

                    if (i >= options.MaxLoad) break;
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
        private async Task<TelegramReply?> AddReplyToDb(MessageBase msg, string userId, long channelId, long msgId)
        {
            var origMsg = msg as TL.Message;

            if (origMsg.message == null || origMsg.message == "") return null;

            var newReply = new TelegramReply()
            {
                Date = origMsg.Date,
                EditDate = origMsg.edit_date,
                LoaderId = userId,
                TelegramID = origMsg.ID,
                Message = origMsg.message,
                TelegramChannelId = channelId,
                TelegramMessageId = msgId,
                GroupedId = origMsg.grouped_id
            };

            await _repository.AddAsync(newReply);

            if (origMsg.reactions != null)
            {
                foreach (var reaction in origMsg.reactions.results)
                {
                    var newReaction = new TelegramReaction()
                    {
                        Count = reaction.count,
                        TelegramReply = newReply
                    };
                    if (reaction.reaction != null)
                    {
                        if (reaction.reaction is TL.ReactionEmoji)
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

            return newReply;
        }

        public async Task LoadFromChannel(LoadTelegramRepliesByChannelOptions options)
        {
            var userId = await _userService.GetCurrentUserId();
            if (userId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.BadRequest);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.LoadingRepliesFromTelegram]);

            _backgroundJobClient.Enqueue(() => LoadRepliesFromChannelBackgroundJob(options, userId, taskId));
        }
        public async Task LoadRepliesFromChannelBackgroundJob(LoadTelegramRepliesByChannelOptions options, string userId, int taskId)
        {
            long channelId = options.ChannelId;
            if (await _channelsRepository.FindAsync(channelId) == null) return;

            var stateId = await _tasksService.GetTaskStateId(taskId);
            if (stateId != TaskStates.Wait) return;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);

            float percent = 0f;

            var logsId = Guid.NewGuid().ToString();
            try
            {
                var maxCount = options.MaxLoad / options.MaxLoadForOneMessage;

                for (int t = 1; t <= maxCount; t++)
                {
                    TelegramChannel channel = await _channelsRepository.FindAsync(options.ChannelId);

                    var resolveRes = await _wTelegramService.Client.Contacts_ResolveUsername(channel.Username);
                    if (resolveRes == null) throw new HttpException(_localizer[ErrorMessagePatterns.TelegramChannelUsernameNotValid], HttpStatusCode.NotFound);

                    Random random = new Random();

                    Expression<Func<TelegramMessage, bool>> filterCondition = e => true;
                    if (options.DateFrom != null) filterCondition = filterCondition.And(e => e.Date > options.DateFrom);
                    if (options.DateTo != null) filterCondition = filterCondition.And(e => e.Date < options.DateTo);

                    var messages = await _messagesRepository.GetAsync(filter: filterCondition, pageParameters: new DTOs.PageParameters()
                    {
                        PageNumber = t,
                        PageSize = 100
                    });
                    if (!messages.Any()) break;

                    List<TelegramMessage> shuffledMessages = messages.OrderBy(x => random.Next()).ToList();

                    int loadedCount = 0;
                    foreach (var message in shuffledMessages)
                    {
                        var origMsgResp = await _wTelegramService.Client.Channels_GetMessages(new InputChannel(channel.TelegramID, resolveRes.Channel.access_hash), new InputMessageID()
                        {
                            id = message.TelegramID
                        });

                        if (origMsgResp.Messages == null ||
                            origMsgResp.Messages.Length == 0 ||
                            !(origMsgResp.Messages[0] is Message) ||
                            (origMsgResp.Messages[0] as Message).replies == null ||
                            (origMsgResp.Messages[0] as Message).replies.replies == 0) continue;

                        int loadedCountInOneMessage = 0;
                        int skipCount = 0;
                        while (true)
                        {
                            var limit = 100;
                            if (options.MaxLoadForOneMessage * 2 < limit) limit = options.MaxLoadForOneMessage * 2;
                            var res = await _wTelegramService.Client.Messages_GetReplies(new InputPeerChannel(channel.TelegramID, resolveRes.Channel.access_hash), message.TelegramID, limit: limit, add_offset: skipCount);

                            var replies = res.Messages;
                            if (replies == null || replies.Length == 0) break;

                            int newRepliesCount = 0;
                            foreach (var reply in replies)
                            {
                                skipCount++;

                                if (loadedCount >= options.MaxLoad) break;
                                if (loadedCountInOneMessage >= options.MaxLoadForOneMessage) break;

                                if (!(reply is TL.Message)) continue;

                                var quotasResult = await _quotasControllerService.TakeCustomerQuotas(userId, QuotasTypes.Telegram, 1, logsId);

                                if (!quotasResult)
                                {
                                    await _tasksService.ChangeTaskState(taskId, TaskStates.Error);
                                    await _tasksService.ChangeTaskDescription(taskId, _localizer[ErrorMessagePatterns.NotEnoughQuotas]);
                                    return;
                                }

                                await AddReplyToDb(reply, userId, channel.Id, message.Id);

                                loadedCount++;
                                loadedCountInOneMessage++;

                                float plusPercent = 100f / options.MaxLoad;
                                percent += plusPercent;

                                await _tasksService.ChangeTaskPercent(taskId, percent);
                                newRepliesCount++;
                            }

                            message.TelegramRepliesCount += newRepliesCount;

                            if (channel != null)
                            {
                                channel.TelegramRepliesCount += newRepliesCount;
                            }

                            await _repository.SaveChangesAsync();

                            if (loadedCount >= options.MaxLoad || loadedCountInOneMessage >= options.MaxLoadForOneMessage) break;
                        }


                        if (loadedCount >= options.MaxLoad) break;
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
    }
}

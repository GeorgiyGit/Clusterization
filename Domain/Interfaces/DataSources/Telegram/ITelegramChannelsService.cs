using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.Entities.DataSources.Telegram;
using Domain.Exceptions;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types.DataSources.Telegram;
using Domain.Resources.Types;
using Domain.Services.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TL;

namespace Domain.Interfaces.DataSources.Telegram
{
    public interface ITelegramChannelsService
    {
        public Task LoadByUsername(string username);
        public Task LoadManyByUsernames(ICollection<string> userNames);

        public Task<FullTelegramChannelDTO> GetLoadedById(long id);
        public Task<ICollection<SimpleTelegramChannelDTO>> GetLoadedCollection(GetTelegramChannelsRequest request);

        public Task<ICollection<SimpleTelegramChannelDTO>> GetCollectionWithoutLoadingByName(string name);
    }
}

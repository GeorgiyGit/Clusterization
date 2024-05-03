using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ChannelDTOs.Responses;
using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DataSourceProfiles.TelegramProfiles
{
    public class TelegramChannelProfile : AutoMapper.Profile
    {
        public TelegramChannelProfile()
        {
            CreateMap<TelegramChannel, SimpleTelegramChannelDTO>();
            CreateMap<TelegramChannel, FullTelegramChannelDTO>();
        }
    }
}

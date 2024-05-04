using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Responses;
using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DataSourceProfiles.TelegramProfiles
{
    public class TelegramMessageProfile:AutoMapper.Profile
    {
        public TelegramMessageProfile()
        {
            CreateMap<TelegramMessage, SimpleTelegramMessageDTO>()
                    .ForMember(dest => dest.IsLoaded,
                               ost => ost.Ignore());

            CreateMap<TelegramMessage, FullTelegramMessageDTO>();
        }
    }
}

using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Responses;
using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DataSourceProfiles.TelegramProfiles
{
    public class TelegramReplyProfile:AutoMapper.Profile
    {
        public TelegramReplyProfile()
        {
            CreateMap<TelegramReply, TelegramReplyDTO>();
        }
    }
}

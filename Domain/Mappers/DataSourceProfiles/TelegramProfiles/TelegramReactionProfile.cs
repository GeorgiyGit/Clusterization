using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReactionDTOs;
using Domain.Entities.DataSources.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.DataSourceProfiles.TelegramProfiles
{
    public class TelegramReactionProfile:AutoMapper.Profile
    {
        public TelegramReactionProfile()
        {
            CreateMap<TelegramReaction, TelegramReactionDTO>();
        }
    }
}

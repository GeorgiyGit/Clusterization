using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Responses;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Telegram;
using Domain.Entities.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataObjectDTOs.Responses
{
    public class FullDataObjectDTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string TypeId { get; set; }

        public YoutubeCommentDTO? YoutubeComment { get; set; }
        public FullTelegramMessageDTO? TelegramMessage { get; set; }
        public TelegramReplyDTO? TelegramReply { get; set; }
        public SimpleExternalObjectDTO? ExternalObject { get; set; }
    }
}

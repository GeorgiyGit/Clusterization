using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReplyDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;

namespace Domain.Interfaces.DataSources.Telegram
{
    public interface ITelegramRepliesService
    {
        public Task LoadFromMessage(TelegramLoadOptions options);
        public Task LoadFromChannel(LoadTelegramRepliesByChannelOptions options);
    }
}

using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Requests;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.MessageDTOs.Responses;
using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;

namespace Domain.Interfaces.DataSources.Telegram
{
    public interface ITelegramMessagesService
    {
        public Task LoadFromChannel(TelegramLoadOptions options);
        public Task LoadFromChannelBackgroundJob(TelegramLoadOptions options, string userId, int taskId);

        public Task LoadById(int id, long channelId);
        public Task LoadManyByIds(ICollection<int> ids, long channelId);

        public Task<FullTelegramMessageDTO> GetLoadedById(long id);
        public Task<ICollection<SimpleTelegramMessageDTO>> GetLoadedCollection(GetTelegramMessagesRequest request);

        public Task<ICollection<SimpleTelegramMessageDTO>> GetCollectionWithoutLoadingByName(GetTelegramMessagesRequest request);
    }
}

using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;

namespace Domain.Interfaces.DataSources.Telegram
{
    public interface ITelegramRepliesDataObjectsService
    {
        public Task LoadRepliesByChannel(AddTelegramRepliesToWorkspaceByChannelRequest request);
        public Task LoadRepliesByMessages(AddTelegramRepliesToWorkspaceByMessagesRequest request);
    }
}

using Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.DataSources.Telegram
{
    public interface ITelegramMessagesDataObjectsService
    {
        public Task LoadMessagesByChannel(AddTelegramMessagesToWorkspaceByChannelRequest request);
    }
}

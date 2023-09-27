using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Youtube
{
    public interface IYoutubeChannelService
    {
        public Task LoadChannel(string id);
        public Task LoadManyByIds(ICollection<string> ids);

        public Task<SimpleChannelDTO> GetLoadedChannelById(string id);
        public Task<ICollection<SimpleChannelDTO>> GetLoadedChannels(GetChannelsRequest request);

        public Task<ChannelsWithoutLoadingResponse> GetChannelsWithoutLoadingByName(string name, string? nextPageToken, string filterType);
    }
}

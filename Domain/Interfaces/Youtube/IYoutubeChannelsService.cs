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
    public interface IYoutubeChannelsService
    {
        public Task LoadById(string id);
        public Task LoadManyByIds(ICollection<string> ids);

        public Task<SimpleChannelDTO> GetLoadedById(string id);
        public Task<ICollection<SimpleChannelDTO>> GetLoadedCollection(GetChannelsRequest request);

        public Task<ChannelsWithoutLoadingResponse> GetCollectionWithoutLoadingByName(string name, string? nextPageToken, string filterType);
    }
}

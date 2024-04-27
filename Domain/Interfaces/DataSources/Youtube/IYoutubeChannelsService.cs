using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeChannelsService
    {
        public Task LoadById(string id);
        public Task LoadManyByIds(ICollection<string> ids);

        public Task<SimpleYoutubeChannelDTO> GetLoadedById(string id);
        public Task<ICollection<SimpleYoutubeChannelDTO>> GetLoadedCollection(GetChannelsRequest request);

        public Task<YoutubeChannelsWithoutLoadingResponse> GetCollectionWithoutLoadingByName(string name, string? nextPageToken, string filterType);
    }
}

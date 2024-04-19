using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeVideoService
    {
        public Task LoadById(string id);
        public Task LoadFromChannel(LoadOptions options);
        public Task LoadManyByIds(ICollection<string> ids);

        public Task<SimpleVideoDTO> GetLoadedById(string id);
        public Task<ICollection<SimpleVideoDTO>> GetLoadedCollection(GetVideosRequest request);

        public Task<VideosWithoutLoadingResponse> GetCollectionWithoutLoadingByName(string name, string? nextPageToken, string? channelId, string filterType);
    }
}

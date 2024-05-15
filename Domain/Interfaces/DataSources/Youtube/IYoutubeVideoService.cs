using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IYoutubeVideoService
    {
        public Task LoadById(string id);
        public Task LoadFromChannel(YoutubeLoadOptions options);
        public Task LoadManyByIds(ICollection<string> ids);

        public Task<SimpleYoutubeVideoDTO> GetLoadedById(string id);
        public Task<ICollection<SimpleYoutubeVideoDTO>> GetLoadedCollection(GetYoutubeVideosRequest request);
        public Task<ICollection<SimpleYoutubeVideoDTO>> GetCustomerLoadedCollection(GetYoutubeVideosRequest request);
        
        public Task<YoutubeVideosWithoutLoadingResponse> GetCollectionWithoutLoadingByName(string name, string? nextPageToken, string? channelId, string filterType);
    }
}

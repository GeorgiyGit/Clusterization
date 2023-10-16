using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Youtube
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

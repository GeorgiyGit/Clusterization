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
        public Task LoadVideoById(string id);
        public Task LoadChannelVideosHangFire(LoadOptions options);
        public Task LoadManyByIds(ICollection<string> ids);

        public Task<SimpleVideoDTO> GetLoadedVideoById(string id);
        public Task<ICollection<SimpleVideoDTO>> GetLoadedVideos(GetVideosRequest request);
        
        public Task<VideosWithoutLoadingResponse> GetVideosWithoutLoadingByName(string name, string? nextPageToken, string? channelId, string filterType);
    }
}

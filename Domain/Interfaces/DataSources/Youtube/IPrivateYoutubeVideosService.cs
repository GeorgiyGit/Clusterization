using Domain.Entities.DataSources.Youtube;

namespace Domain.Interfaces.DataSources.Youtube
{
    public interface IPrivateYoutubeVideosService
    {
        public Task<Video?> GetById(string id);
    }
}

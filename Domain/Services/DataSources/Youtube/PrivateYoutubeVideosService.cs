using Domain.Interfaces.Other;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class PrivateYoutubeVideosService : IPrivateYoutubeVideosService
    {
        private readonly IRepository<YoutubeVideo> _repository;
        public PrivateYoutubeVideosService(IRepository<YoutubeVideo> repository)
        {
            _repository = repository;
        }
        public async Task<YoutubeVideo?> GetById(string id)
        {
            return (await _repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(YoutubeVideo.Channel)}", pageParameters: null)).FirstOrDefault();
        }
    }
}

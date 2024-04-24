using Domain.Interfaces.Other;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class PrivateYoutubeVideosService : IPrivateYoutubeVideosService
    {
        private readonly IRepository<Video> _repository;
        public PrivateYoutubeVideosService(IRepository<Video> repository)
        {
            _repository = repository;
        }
        public async Task<Video?> GetById(string id)
        {
            return (await _repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Video.Channel)}", pageParameters: null)).FirstOrDefault();
        }
    }
}

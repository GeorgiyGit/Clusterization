using Domain.Interfaces.Other;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Entities.DataSources.Youtube;

namespace Domain.Services.DataSources.Youtube
{
    public class PrivateYoutubeVideosService : IPrivateYoutubeVideosService
    {
        private readonly IRepository<Video> repository;
        public PrivateYoutubeVideosService(IRepository<Video> repository)
        {
            this.repository = repository;
        }
        public async Task<Video?> GetById(string id)
        {
            return (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Video.Channel)}", pageParameters: null)).FirstOrDefault();
        }
    }
}

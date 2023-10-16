using Domain.Entities.Youtube;
using Domain.Interfaces;
using Domain.Interfaces.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Youtube
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
            return (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Video.Channel)}")).FirstOrDefault();
        }
    }
}

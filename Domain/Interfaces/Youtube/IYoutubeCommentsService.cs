using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Youtube
{
    public interface IYoutubeCommentsService
    {
        public Task LoadFromVideo(LoadOptions options);

        public Task<CommentDTO> GetLoadedById(string commentId);
        public Task<ICollection<CommentDTO>> GetLoadedCollection(GetCommentsRequest request);
    }
}

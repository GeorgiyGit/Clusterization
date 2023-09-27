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
        public Task LoadCommentsFromVideo(LoadOptions options);

        public Task<CommentDTO> GetLoadedCommentById(string commentId);
        public Task<ICollection<CommentDTO>> GetLoadedComments(GetCommentsRequest request);
    }
}

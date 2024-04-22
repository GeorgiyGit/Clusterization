using Domain.DTOs.EmbeddingDTOs.Responses;
using Domain.Entities.Embeddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.EmbeddingProfiles
{
    public class EmbeddingLoadingStatesProfile:AutoMapper.Profile
    {
        public EmbeddingLoadingStatesProfile()
        {
            CreateMap<EmbeddingLoadingState, EmbeddingLoadingStateDTO>();
        }
    }
}

using Domain.DTOs.EmbeddingModelDTOs.Responses;
using Domain.Entities.EmbeddingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.EmbeddingModels
{
    public class EmbeddingModelProfile:AutoMapper.Profile
    {
        public EmbeddingModelProfile()
        {
            CreateMap<EmbeddingModel, EmbeddingModelDTO>();
        }
    }
}

using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles
{
    internal class ClusterizationWorkspaceMapperProfile : AutoMapper.Profile
    {
        public ClusterizationWorkspaceMapperProfile()
        {
            CreateMap<ClusterizationWorkspace, ClusterizationWorkspaceDTO>()
                                               .ForMember(dest => dest.TypeName,
                                                          ost => ost.MapFrom(e => e.Type.Name))
                                               .ForMember(dest => dest.ProfilesCount,
                                                          ost => ost.MapFrom(e => e.Profiles.Count))
                                               .ForMember(dest => dest.CommentsCount,
                                                          ost => ost.MapFrom(e => (e.Comments.Count + e.ExternalObjects.Count)));

            CreateMap<ClusterizationWorkspace, SimpleClusterizationWorkspaceDTO>()
                                               .ForMember(dest => dest.TypeName,
                                                          ost => ost.MapFrom(e => e.Type.Name));
        }
    }
}

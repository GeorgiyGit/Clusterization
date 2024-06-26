﻿using Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.ModelDTOs;
using Domain.DTOs.YoutubeDTOs.CommentDTOs;
using Domain.Entities.Clusterization.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles.Workspaces
{
    internal class ClusterizationWorkspaceMapperProfile : AutoMapper.Profile
    {
        public ClusterizationWorkspaceMapperProfile()
        {
            CreateMap<ClusterizationWorkspace, ClusterizationWorkspaceDTO>()
                                               .ForMember(dest => dest.TypeName,
                                                          ost => ost.MapFrom(e => e.Type.Name))
                                               .ForMember(dest => dest.ProfilesCount,
                                                          ost => ost.MapFrom(e => e.Profiles.Count));

            CreateMap<ClusterizationWorkspace, SimpleClusterizationWorkspaceDTO>()
                                               .ForMember(dest => dest.TypeName,
                                                          ost => ost.MapFrom(e => e.Type.Name));
        }
    }
}

using Domain.DTOs.ClusterizationDTOs.WorkspaceAddPackDTOs.Respones;
using Domain.Entities.Clusterization.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.ClusterizationProfiles.Workspaces
{
    public class WorkspaceAddPackProfile:AutoMapper.Profile
    {
        public WorkspaceAddPackProfile()
        {
            CreateMap<WorkspaceDataObjectsAddPack, WorkspaceDataObjectsAddPackSimpleDTO>()
                .ForMember(dest => dest.WorkspaceChangingType,
                           ost => ost.MapFrom(e => e.Workspace.ChangingType));

            CreateMap<WorkspaceDataObjectsAddPack, WorkspaceDataObjectsAddPackFullDTO>()
                .ForMember(dest => dest.EmbeddingLoadingStates,
                           ost => ost.Ignore())
                .ForMember(dest => dest.WorkspaceChangingType,
                           ost => ost.MapFrom(e => e.Workspace.ChangingType));
        }
    }
}

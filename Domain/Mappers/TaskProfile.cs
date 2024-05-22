using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.Responses;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    internal class TaskProfile : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<MyBaseTask, TaskDTO>()
                    .ForMember(dest => dest.StateName,
                               ost => ost.MapFrom(e => e.State.Name));

            CreateMap<MyBaseTask, FullTaskDTO>()
                    .ForMember(dest => dest.StateName,
                               ost => ost.MapFrom(e => e.State.Name));

            CreateMap<MyMainTask, MainTaskDTO>()
                    .ForMember(dest => dest.StateName,
                               ost => ost.MapFrom(e => e.State.Name))
                    .ForMember(dest => dest.SubTasks,
                               ost => ost.Ignore());

            CreateMap<MyBaseTask, SimpleSubTaskDTO>();

            CreateMap<MyBaseTask, SubTaskDTO>()
                    .ForMember(dest => dest.StateName,
                               ost => ost.MapFrom(e => e.State.Name));
        }
    }
}

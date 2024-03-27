using Domain.DTOs.TaskDTOs;
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
            CreateMap<MyTask, TaskDTO>()
                    .ForMember(dest => dest.StateName,
                               ost => ost.MapFrom(e => e.State.Name));

            CreateMap<MyTask, FullTaskDTO>()
                    .ForMember(dest => dest.StateName,
                               ost => ost.MapFrom(e => e.State.Name));
        }
    }
}

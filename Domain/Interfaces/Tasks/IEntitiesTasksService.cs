using Domain.DTOs.TaskDTOs.GetEntitiesTaskDTOs;
using Domain.DTOs.TaskDTOs.Responses;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types.Clusterization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Tasks
{
    public interface IEntitiesTasksService
    {
        public Task<ICollection<MainTaskDTO>> GetProfileTasks(GetEntityTasksRequest<int> request);
        public Task<ICollection<MainTaskDTO>> GetWorkspaceTasks(GetEntityTasksRequest<int> request);

        public Task<ICollection<MainTaskDTO>> GetYoutubeChannelTasks(GetEntityTasksRequest<string> request);
        public Task<ICollection<MainTaskDTO>> GetYoutubeVideoTasks(GetEntityTasksRequest<string> request);

        public Task<ICollection<MainTaskDTO>> GetTelegramChannelTasks(GetEntityTasksRequest<long> request);
        public Task<ICollection<MainTaskDTO>> GetTelegramMessageTasks(GetEntityTasksRequest<long> request);

        public Task<ICollection<MainTaskDTO>> GetExternalObjectsPackTasks(GetEntityTasksRequest<int> request);
    }
}

using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.DTOs.TaskDTOs.Responses;

namespace Domain.Interfaces.Tasks
{
    public interface IModeratorTasksService
    {
        public Task<ICollection<MainTaskDTO>> GetMainTasks(ModeratorGetTasksRequest request);
        public Task<ICollection<SubTaskDTO>> GetSubTasks(ModeratorGetSubTasksRequest request);
    }
}

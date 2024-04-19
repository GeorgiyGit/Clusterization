using Domain.DTOs.TaskDTOs;

namespace Domain.Interfaces.Tasks
{
    public interface IModeratorTasksService
    {
        public Task<ICollection<TaskDTO>> GetTasks(ModeratorGetTasksRequest request);
    }
}

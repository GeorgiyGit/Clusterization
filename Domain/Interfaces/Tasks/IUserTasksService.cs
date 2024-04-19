using Domain.DTOs.TaskDTOs;

namespace Domain.Interfaces.Tasks
{
    public interface IUserTasksService
    {
        public Task<FullTaskDTO> GetFullTask(int id);
        public Task<ICollection<TaskDTO>> GetTasks(CustomerGetTasksRequest request);
    }
}

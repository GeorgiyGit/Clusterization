using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.DTOs.TaskDTOs.Responses;

namespace Domain.Interfaces.Tasks
{
    public interface IUserTasksService
    {
        public Task<FullTaskDTO> GetFullTask(string id);
        public Task<ICollection<MainTaskDTO>> GetMainTasks(CustomerGetTasksRequest request);
        public Task<ICollection<SubTaskDTO>> GetSubTasks(CustomerGetSubTasksRequest request);
    }
}

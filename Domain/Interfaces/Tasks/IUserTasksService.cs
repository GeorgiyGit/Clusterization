using Domain.DTOs.TaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Tasks
{
    public interface IUserTasksService
    {
        public Task<FullTaskDTO> GetFullTask(int id);
        public Task<ICollection<TaskDTO>> GetTasks(CustomerGetTasksRequest request);
    }
}

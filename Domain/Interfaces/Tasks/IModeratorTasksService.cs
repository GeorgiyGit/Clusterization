using Domain.DTOs.TaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Tasks
{
    public interface IModeratorTasksService
    {
        public Task<ICollection<TaskDTO>> GetTasks(ModeratorGetTasksRequest request);
    }
}

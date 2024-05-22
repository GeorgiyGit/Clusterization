using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Responses
{
    public class MainTaskDTO: TaskDTO
    {
        public bool IsGroupTask { get; set; }

        public int SubTasksCount { get; set; }
        public ICollection<SimpleSubTaskDTO> SubTasks { get; set; } = new List<SimpleSubTaskDTO>();
    }
}

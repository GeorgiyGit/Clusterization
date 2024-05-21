using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Responses
{
    public class SubTaskDTO:TaskDTO
    {
        public int Position { get; set; }
        public string GroupTaskId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs
{
    public class ModeratorGetTasksRequest
    {
        public string? TaskStateId { get; set; }
        public string? CustomerId { get; set; }
        public PageParameters PageParameters { get; set; }
    }
}

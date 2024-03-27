using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs
{
    public class CustomerGetTasksRequest
    {
        public string? TaskStateId { get; set; }
        public PageParametersDTO PageParameters { get; set; }
    }
}

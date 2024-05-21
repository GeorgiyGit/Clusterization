using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Requests
{
    public class CustomerGetSubTasksRequest: CustomerGetTasksRequest
    {
        public string GroupTaskId { get; set; }
    }
}

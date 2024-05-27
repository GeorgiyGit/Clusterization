using Domain.DTOs.SignalRDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.SignalRDTOs
{
    public class ChangeTaskStateEvent: BaseSignalRResponse
    {
        public string TaskId { get; set; }
        public string? GroupTaskId { get; set; }
        public string StateName { get; set; }
        public string StateId { get; set; }
    }
}

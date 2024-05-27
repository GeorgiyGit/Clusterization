using Domain.DTOs.SignalRDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.SignalRDTOs
{
    public class ChangeTaskPercentsEvent: BaseSignalRResponse
    {
        public string TaskId { get; set; }
        public string? GroupTaskId { get; set; }

        public double Percents { get; set; }
    }
}

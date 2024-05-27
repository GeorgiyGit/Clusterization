using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Responses
{
    public class SimpleSubTaskDTO
    {
        public string Id { get; set; }
        public int Position { get; set; }
        public string StateId { get; set; }
    }
}

using Domain.Entities.Tasks;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public float Percent { get; set; }

        public string StateName { get; set; }
        public string StateId { get; set; }
    }
}

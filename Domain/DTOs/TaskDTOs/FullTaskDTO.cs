using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs
{
    public class FullTaskDTO
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public float Percent { get; set; }

        public string StateName { get; set; }
        public string StateId { get; set; }

        public string? Description { get; set; }
        public string CustomerId { get; set; }
    }
}

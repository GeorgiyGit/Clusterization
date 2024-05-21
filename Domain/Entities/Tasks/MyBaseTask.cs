using Domain.Entities.Customers;
using Domain.Resources.Types.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tasks
{
    public class MyBaseTask
    {
        public string Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public MyTaskState State { get; set; }
        public string StateId { get; set; }

        public string? Description { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public string? EntityType { get; set; }
        public string? EntityId { get; set; }

        public float Percent { get; set; }

        public string TaskType { get; set; } = TaskTypes.MainTask;
    }
}

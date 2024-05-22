using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.Requests
{
    public class CreateSubTaskOptions
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }
        public string? Description { get; set; }

        public string StateId { get; set; }

        public string CustomerId { get; set; }

        public string? EntityType { get; set; }
        public string? EntityId { get; set; }

        public string GroupTaskId { get; set; }

        public int Position { get; set; }
        public bool IsPercents { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TaskDTOs.GetEntitiesTaskDTOs
{
    public class GetEntityTasksRequest<EntityId>
    {
        public EntityId Id { get; set; }
        public PageParameters PageParameters { get; set; }
        public string? TaskStateId { get; set; }
    }
}

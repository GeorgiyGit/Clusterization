
namespace Domain.DTOs.TaskDTOs
{
    public class FullTaskDTO : TaskDTO
    {
        public string? Description { get; set; }
        public string CustomerId { get; set; }
    }
}

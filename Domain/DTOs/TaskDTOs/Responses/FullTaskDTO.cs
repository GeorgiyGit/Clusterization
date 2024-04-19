
namespace Domain.DTOs.TaskDTOs
{
    public class FullTaskDTO
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public float Percent { get; set; }

        public string StateName { get; set; }
        public string StateId { get; set; }

        public string? Description { get; set; }
        public string CustomerId { get; set; }
    }
}

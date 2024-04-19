using Domain.Entities.Customers;

namespace Domain.Entities.Tasks
{
    public class MyTask
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public float Percent { get; set; }

        public MyTaskState State { get; set; }
        public string StateId { get; set; }

        public string? Description { get; set; }

        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
    }
}

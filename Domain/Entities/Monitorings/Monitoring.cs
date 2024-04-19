
namespace Domain.Entities.Monitorings
{
    public class Monitoring : IMonitoring
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public DateTime? LastEditTime { get; set; }
        public DateTime? LastDeleteTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
    }
}


namespace Domain.Entities.Monitorings
{
    public interface IMonitoring
    {
        public DateTime CreationTime { get; set; }

        public DateTime? LastEditTime { get; set; }
        public DateTime? LastDeleteTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
    }
}

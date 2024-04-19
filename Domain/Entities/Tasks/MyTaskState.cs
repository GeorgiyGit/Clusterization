
namespace Domain.Entities.Tasks
{
    public class MyTaskState
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<MyTask> Tasks { get; set; } = new HashSet<MyTask>();
    }
}

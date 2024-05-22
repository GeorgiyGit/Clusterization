using Domain.Entities.Customers;

namespace Domain.Entities.Tasks
{
    public class MyMainTask : MyBaseTask
    {
        public bool IsGroupTask { get; set; }

        public int SubTasksCount { get; set; }
        public ICollection<MySubTask> SubTasks { get; set; } = new HashSet<MySubTask>();
    }
}

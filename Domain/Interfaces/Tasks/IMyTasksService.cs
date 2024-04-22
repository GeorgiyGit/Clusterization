
namespace Domain.Interfaces.Tasks
{
    public interface IMyTasksService
    {
        public Task<int> CreateTask(string name);
        public Task<int> CreateTaskWithUserId(string name, string userId);

        public Task ChangeTaskState(int id, string newStateId);
        public Task ChangeTaskPercent(int id, float newPercent);
        public Task ChangeTaskDescription(int id,string description);

        public Task<string?> GetTaskStateId(int id);
    }
}

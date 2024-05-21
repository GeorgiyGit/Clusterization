
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using System.Net;

namespace Domain.Interfaces.Tasks
{
    public interface IMyTasksService
    {
        public Task ChangeTaskPercent(string id, float newPercent);
        public Task ChangeTaskState(string id, string newStateId);
        public Task ChangeParentTaskState(string id, string newStateId);
        public Task ChangeTaskDescription(string id, string description);

        public Task<string> CreateMainTask(CreateMainTaskOptions options);
        public Task<string> CreateMainTaskWithUserId(CreateMainTaskOptions options);

        public Task<string> CreateSubTask(CreateSubTaskOptions options);
        public Task<string> CreateSubTaskWithUserId(CreateSubTaskOptions options);

        public Task<string?> GetTaskStateId(string id);
    }
}

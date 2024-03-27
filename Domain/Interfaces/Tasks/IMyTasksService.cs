using Domain.DTOs.TaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Tasks
{
    public interface IMyTasksService
    {
        public Task<int> CreateTask(string name);

        public Task ChangeTaskState(int id, string newStateId);
        public Task ChangeTaskPercent(int id, float newPercent);
        public Task ChangeTaskDescription(int id,string description);

        public Task<string?> GetTaskStateId(int id);
    }
}

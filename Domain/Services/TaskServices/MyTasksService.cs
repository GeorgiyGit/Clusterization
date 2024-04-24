using AutoMapper;
using Domain.DTOs.TaskDTOs;
using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TaskServices
{
    public class MyTasksService : IMyTasksService
    {
        private readonly IRepository<MyTask> _tasksRepository;
        private readonly IRepository<MyTaskState> _statesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IUserService _userService;

        public MyTasksService(IRepository<MyTask> task_repository,
                             IRepository<MyTaskState> state_repository,
                             IUserService userService,
                             IStringLocalizer<ErrorMessages> localizer)
        {
            _tasksRepository = task_repository;
            _statesRepository = state_repository;
            _userService = userService;
            _localizer = localizer;
        }

        public async Task ChangeTaskPercent(int id, float newPercent)
        {
            var task = (await _tasksRepository.GetAsync(e=>e.Id==id,includeProperties:$"{nameof(MyTask.State)}")).FirstOrDefault();

            if (task == null) return;

            task.Percent = newPercent;

            await _tasksRepository.SaveChangesAsync();
        }

        public async Task ChangeTaskState(int id, string newStateId)
        {
            var task = (await _tasksRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyTask.State)}")).FirstOrDefault();
            if (task == null) return;

            var taskState = await _statesRepository.FindAsync(newStateId);
            if(taskState == null) return;

            if (newStateId == TaskStates.Completed) task.EndTime = DateTime.UtcNow;

            task.StateId = newStateId;
            task.State = taskState;

            await _tasksRepository.SaveChangesAsync(); 
        }

        public async Task<int> CreateTask(string name)
        {
            var customerId = await _userService.GetCurrentUserId();

            if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.Unauthorized);

            return await CreateTaskWithUserId(name, customerId);
        }
        public async Task<int> CreateTaskWithUserId(string name, string userId)
        {
            var state = await _statesRepository.FindAsync(TaskStates.Wait);

            var task = new MyTask()
            {
                Title = name,
                Percent = 0,
                StateId = state.Id,
                CustomerId = userId
            };

            await _tasksRepository.AddAsync(task);
            await _tasksRepository.SaveChangesAsync();

            return task.Id;
        }

        public async Task<string?> GetTaskStateId(int id)
        {
            var task = (await _tasksRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyTask.State)}")).FirstOrDefault();

            return task?.StateId;
        }
        public async Task ChangeTaskDescription(int id, string description)
        {
            var task = (await _tasksRepository.GetAsync(e => e.Id == id)).FirstOrDefault();
            if (task == null) return;

            task.Description = description;

            await _tasksRepository.SaveChangesAsync();
        }
    }
}

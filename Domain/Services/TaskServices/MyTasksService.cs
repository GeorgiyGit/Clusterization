using AutoMapper;
using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Resources.Types.Tasks;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TaskServices
{
    public class MyTasksService : IMyTasksService
    {
        private readonly IRepository<MyBaseTask> _baseTasksRepository;
        private readonly IRepository<MyMainTask> _mainTasksRepository;
        private readonly IRepository<MySubTask> _subTasksRepository;
        private readonly IRepository<MyTaskState> _statesRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IUserService _userService;

        public MyTasksService(IRepository<MyBaseTask> baseTasksRepository,
                              IRepository<MySubTask> subTasksRepository,
                              IRepository<MyMainTask> mainTasksRepository,
                              IRepository<MyTaskState> state_repository,
                              IUserService userService,
                              IStringLocalizer<ErrorMessages> localizer)
        {
            _baseTasksRepository = baseTasksRepository;
            _mainTasksRepository = mainTasksRepository;
            _subTasksRepository = subTasksRepository;
            _statesRepository = state_repository;
            _userService = userService;
            _localizer = localizer;
        }


        public async Task ChangeTaskPercent(string id, float newPercent)
        {
            var task = await _baseTasksRepository.FindAsync(id);

            if (task == null) return;

            task.Percent = newPercent;

            await _baseTasksRepository.SaveChangesAsync();
        }
        public async Task ChangeTaskState(string id, string newStateId)
        {
            var task = (await _baseTasksRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyBaseTask.State)}")).FirstOrDefault();
            if (task == null) return;

            var taskState = await _statesRepository.FindAsync(newStateId);
            if(taskState == null) return;

            if (newStateId == TaskStates.Completed)
            {
                task.EndTime = DateTime.UtcNow;
            }
            else if (newStateId == TaskStates.Process)
            {
                task.StartTime = DateTime.UtcNow;
            }
            else if (newStateId == TaskStates.Error)
            {
                if (task.TaskType == TaskTypes.MainTask)
                {
                    var subTasks = await _subTasksRepository.GetAsync(e => e.GroupTaskId == task.Id, includeProperties: $"{nameof(MyBaseTask.State)}");

                    foreach (var subTask in subTasks)
                    {
                        if (subTask.StateId == TaskStates.Process)
                        {
                            subTask.StateId = TaskStates.Stopped;
                        }
                    }
                }
            }

            task.StateId = newStateId;
            task.State = taskState;

            await _baseTasksRepository.SaveChangesAsync(); 
        }
        public async Task ChangeParentTaskState(string id, string newStateId)
        {
            var task = (await _baseTasksRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyBaseTask.State)}")).FirstOrDefault();
            if (task == null) return;

            var taskState = await _statesRepository.FindAsync(newStateId);
            if (taskState == null) return;

            if (task.TaskType == TaskTypes.SubTask)
            {
                var originalTask = await _subTasksRepository.FindAsync(id);
                if (originalTask == null) return;

                var groupTask = (await _mainTasksRepository.GetAsync(e => e.Id == originalTask.GroupTaskId, includeProperties: $"{nameof(MyBaseTask.State)}")).FirstOrDefault();
                if (groupTask == null) return;

                groupTask.StateId = newStateId;
                groupTask.State = taskState;

                if (newStateId == TaskStates.Completed)
                {
                    task.EndTime = DateTime.UtcNow;
                }
                else if (newStateId == TaskStates.Error)
                {
                    var subTasks = await _subTasksRepository.GetAsync(e => e.GroupTaskId == task.Id, includeProperties: $"{nameof(MyBaseTask.State)}");

                    foreach (var subTask in subTasks)
                    {
                        if (subTask.StateId == TaskStates.Process)
                        {
                            subTask.StateId = TaskStates.Stopped;
                        }
                    }
                }

                await _baseTasksRepository.SaveChangesAsync();
            }
        }
        public async Task ChangeTaskDescription(string id, string description)
        {
            var task = await _baseTasksRepository.FindAsync(id);
            if (task == null) return;

            task.Description = description;

            await _baseTasksRepository.SaveChangesAsync();
        }
        public async Task ChangeTaskReferences(string id, ChangeTaskReferencesRequest request)
        {
            var task = await _baseTasksRepository.FindAsync(id);
            if (task == null) return;

            task.YoutubeChannelId = request.YoutubeChannelId;
            task.YoutubeVideoId = request.YoutubeVideoId;

            task.TelegramChannelId = request.TelegramChannelId;
            task.TelegramMessageId = request.TelegramMessageId;

            task.ExternalObjectsPackId= request.ExternalObjectsPackId;

            task.FastClusteringWorkflowId= request.FastClusteringWorkflowId;
            task.ClusterizationProfileId = request.ClusterizationProfileId;
            task.WorkspaceId = request.WorkspaceId;
            task.AddPackId = request.AddPackId;

            await _baseTasksRepository.SaveChangesAsync();
        }

        public async Task<string> CreateMainTask(CreateMainTaskOptions options)
        {
            var customerId = await _userService.GetCurrentUserId();

            if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.Unauthorized);

            options.CustomerId = customerId;

            return await CreateMainTaskWithUserId(options);
        }
        public async Task<string> CreateMainTaskWithUserId(CreateMainTaskOptions options)
        {
            var task = new MyMainTask()
            {
                Title = options.Title,
                Description = options.Description,
                WorkspaceId=options.WorkspaceId,
                ClusterizationProfileId=options.ClusterizationProfileId,
                ExternalObjectsPackId=options.ExternalObjectsPackId,
                AddPackId=options.AddPackId,
                FastClusteringWorkflowId=options.FastClusteringWorkflowId,
                TelegramChannelId=options.TelegramChannelId,
                TelegramMessageId=options.TelegramMessageId,
                YoutubeChannelId=options.YoutubeChannelId,
                YoutubeVideoId=options.YoutubeVideoId,
                Id = options.Id,
                IsGroupTask = options.IsGroupTask,
                Percent = 0,
                CustomerId = options.CustomerId,
                TaskType = TaskTypes.MainTask,
                IsPercents = options.IsPercents
            };

            if (options.StateId != null) task.StateId = options.StateId;
            else task.StateId = TaskStates.Wait;

            await _mainTasksRepository.AddAsync(task);
            await _mainTasksRepository.SaveChangesAsync();

            return task.Id;
        }

        public async Task<string> CreateSubTask(CreateSubTaskOptions options)
        {
            var customerId = await _userService.GetCurrentUserId();

            if (customerId == null) throw new HttpException(_localizer[ErrorMessagePatterns.UserNotAuthorized], HttpStatusCode.Unauthorized);

            options.CustomerId = customerId;

            return await CreateSubTaskWithUserId(options);
        }
        public async Task<string> CreateSubTaskWithUserId(CreateSubTaskOptions options)
        {
            var task = new MySubTask()
            {
                Title = options.Title,
                Description = options.Description,
                Id = options.Id,
                GroupTaskId = options.GroupTaskId,
                Percent = 0,
                CustomerId = options.CustomerId,
                Position = options.Position,
                TaskType = TaskTypes.SubTask,
                WorkspaceId = options.WorkspaceId,
                ClusterizationProfileId = options.ClusterizationProfileId,
                ExternalObjectsPackId = options.ExternalObjectsPackId,
                AddPackId = options.AddPackId,
                FastClusteringWorkflowId = options.FastClusteringWorkflowId,
                TelegramChannelId = options.TelegramChannelId,
                TelegramMessageId = options.TelegramMessageId,
                YoutubeChannelId = options.YoutubeChannelId,
                YoutubeVideoId = options.YoutubeVideoId,
                IsPercents = options.IsPercents
            };

            if (options.StateId != null) task.StateId = options.StateId;
            else task.StateId = TaskStates.Wait;

            await _subTasksRepository.AddAsync(task);
            await _subTasksRepository.SaveChangesAsync();

            return task.Id;
        }

        public async Task<string?> GetTaskStateId(string id)
        {
            var task = await _baseTasksRepository.FindAsync(id);

            return task?.StateId;
        }
    }
}

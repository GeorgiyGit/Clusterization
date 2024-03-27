﻿using AutoMapper;
using Domain.DTOs.TaskDTOs;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Interfaces;
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
        private readonly IRepository<MyTask> task_repository;
        private readonly IRepository<MyTaskState> state_repository;

        public MyTasksService(IRepository<MyTask> task_repository,
                             IRepository<MyTaskState> state_repository)
        {
            this.task_repository = task_repository;
            this.state_repository = state_repository;
        }

        public async Task ChangeTaskPercent(int id, float newPercent)
        {
            var task = (await task_repository.GetAsync(e=>e.Id==id,includeProperties:$"{nameof(MyTask.State)}")).FirstOrDefault();

            if (task == null) return;

            task.Percent = newPercent;

            await task_repository.SaveChangesAsync();
        }

        public async Task ChangeTaskState(int id, string newStateId)
        {
            var task = (await task_repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyTask.State)}")).FirstOrDefault();
            if (task == null) return;

            var taskState = await state_repository.FindAsync(newStateId);
            if(taskState == null) return;

            task.StateId = newStateId;
            task.State = taskState;

            await task_repository.SaveChangesAsync(); 
        }

        public async Task<int> CreateTask(string name)
        {
            var state = await state_repository.FindAsync(TaskStates.Wait);

            var task = new MyTask()
            {
                Title = name,
                Percent = 0,
                StateId = state.Id
            };

            await task_repository.AddAsync(task);
            await task_repository.SaveChangesAsync();

            return task.Id;
        }
    }
}

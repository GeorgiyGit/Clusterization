using AutoMapper;
using Domain.DTOs.TaskDTOs;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Extensions;
using Domain.Entities.Tasks;
using Domain.Interfaces.Other;
using Domain.DTOs.TaskDTOs.Responses;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.Services.Customers;

namespace Domain.Services.TaskServices
{
    public class ModeratorTasksService : IModeratorTasksService
    {
        private readonly IRepository<MyBaseTask> _baseTasksRepository;
        private readonly IRepository<MyMainTask> _mainTasksRepository;
        private readonly IRepository<MySubTask> _subTasksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;

        public ModeratorTasksService(IRepository<MyBaseTask> baseTasksRepository,
            IRepository<MyMainTask> mainTasksRepository,
            IRepository<MySubTask> subTasksRepository,
            IMapper mapper,
            IStringLocalizer<ErrorMessages> localizer)
        {
            _baseTasksRepository = baseTasksRepository;
            _mainTasksRepository = mainTasksRepository;
            _subTasksRepository = subTasksRepository;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<ICollection<MainTaskDTO>> GetMainTasks(ModeratorGetTasksRequest request)
        {
            Expression<Func<MyMainTask, bool>> filterCondition = e => true;

            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }

            if (request.CustomerId != null)
            {
                filterCondition = filterCondition.And(e => e.CustomerId == request.CustomerId);
            }

            var tasks = (await _mainTasksRepository.GetAsync(filter: filterCondition,
                                                       orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                       includeProperties: $"{nameof(MyBaseTask.State)}",
                                                       pageParameters: request.PageParameters)).ToList();

            var mappedTasks = new List<MainTaskDTO>(tasks.Count);

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];

                var subTasks = await _subTasksRepository.GetAsync(e => e.GroupTaskId == task.Id,
                                                                  includeProperties: $"{nameof(MyBaseTask.State)}");

                var mappedSubTasks = _mapper.Map<ICollection<SimpleSubTaskDTO>>(subTasks);

                var mappedTask = _mapper.Map<MainTaskDTO>(task);
                mappedTask.SubTasks = mappedSubTasks;
                mappedTasks.Add(mappedTask);
            }

            return mappedTasks;
        }
        public async Task<ICollection<SubTaskDTO>> GetSubTasks(ModeratorGetSubTasksRequest request)
        {
            Expression<Func<MySubTask, bool>> filterCondition = e => e.GroupTaskId == request.GroupTaskId;

            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }
            if (request.CustomerId != null)
            {
                filterCondition = filterCondition.And(e => e.CustomerId == request.CustomerId);
            }

            var tasks = (await _subTasksRepository.GetAsync(filter: filterCondition,
                                                       orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                       includeProperties: $"{nameof(MyBaseTask.State)}",
                                                       pageParameters: request.PageParameters)).ToList();

            return _mapper.Map<ICollection<SubTaskDTO>>(tasks);
        }
    }
}

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

namespace Domain.Services.TaskServices
{
    public class ModeratorTasksService : IModeratorTasksService
    {
        private readonly IRepository<MyTask> _tasksRepository;
        private readonly IMapper _mapper;

        public ModeratorTasksService(IRepository<MyTask> task_repository,
                                     IMapper mapper)
        {
            _tasksRepository = task_repository;
            _mapper = mapper;
        }
        public async Task<ICollection<TaskDTO>> GetTasks(ModeratorGetTasksRequest request)
        {
            Expression<Func<MyTask, bool>> filterCondition = e => true;

            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }

            if (request.CustomerId != null)
            {
                filterCondition = filterCondition.And(e => e.CustomerId == request.CustomerId);
            }

            var tasks = await _tasksRepository.GetAsync(filter: filterCondition,
                                                       orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                       includeProperties: $"{nameof(MyTask.State)}",
                                                       pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<TaskDTO>>(tasks);
        }
    }
}

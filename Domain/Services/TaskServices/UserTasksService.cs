using AutoMapper;
using Domain.DTOs.TaskDTOs;
using Domain.DTOs.TaskDTOs.Requests;
using Domain.DTOs.TaskDTOs.Responses;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;

namespace Domain.Services.TaskServices
{
    public class UserTasksService : IUserTasksService
    {
        private readonly IRepository<MyBaseTask> _baseTasksRepository;
        private readonly IRepository<MyMainTask> _mainTasksRepository;
        private readonly IRepository<MySubTask> _subTasksRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserTasksService(IRepository<MyBaseTask> baseTasksRepository,
            IRepository<MyMainTask> mainTasksRepository,
            IRepository<MySubTask> subTasksRepository,
            IMapper mapper,
            IStringLocalizer<ErrorMessages> localizer,
            IUserService userService)
        {
            _baseTasksRepository = baseTasksRepository;
            _mainTasksRepository = mainTasksRepository;
            _subTasksRepository = subTasksRepository;
            _mapper = mapper;
            _localizer = localizer;
            _userService = userService;
        }
        public async Task<ICollection<MainTaskDTO>> GetMainTasks(CustomerGetTasksRequest request)
        {
            var customerId = await _userService.GetCurrentUserId();

            Expression<Func<MyMainTask, bool>> filterCondition = e => e.CustomerId == customerId;

            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }

            var tasks = (await _mainTasksRepository.GetAsync(filter: filterCondition,
                                                       orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                       includeProperties: $"{nameof(MyBaseTask.State)}",
                                                       pageParameters: request.PageParameters)).ToList();

            var mappedTasks = new List<MainTaskDTO>(tasks.Count);

            for(int i = 0; i < tasks.Count; i++)
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
        public async Task<ICollection<SubTaskDTO>> GetSubTasks(CustomerGetSubTasksRequest request)
        {
            var customerId = await _userService.GetCurrentUserId();

            Expression<Func<MySubTask, bool>> filterCondition = e => e.CustomerId == customerId && e.GroupTaskId == request.GroupTaskId;

            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }

            var tasks = (await _subTasksRepository.GetAsync(filter: filterCondition,
                                                       orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                       includeProperties: $"{nameof(MyBaseTask.State)}",
                                                       pageParameters: request.PageParameters)).ToList();

            return _mapper.Map<ICollection<SubTaskDTO>>(tasks);
        }

        public async Task<FullTaskDTO> GetFullTask(string id)
        {
            var task = (await _baseTasksRepository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyBaseTask.State)},{nameof(MyBaseTask.Customer)}")).FirstOrDefault();

            if (task == null) throw new HttpException(_localizer[ErrorMessagePatterns.TaskNotFound], HttpStatusCode.NotFound);

            return _mapper.Map<FullTaskDTO>(task);
        }
    }
}

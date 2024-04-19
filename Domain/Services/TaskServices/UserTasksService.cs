using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.TaskDTOs;
using Domain.Entities.Customers;
using Domain.Entities.Tasks;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using System.Net;

namespace Domain.Services.TaskServices
{
    public class UserTasksService : IUserTasksService
    {
        private readonly IRepository<MyTask> task_repository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<ErrorMessages> localizer;
        private readonly IUserService _userService;

        public UserTasksService(IRepository<MyTask> task_repository,
                                IMapper mapper,
                                IStringLocalizer<ErrorMessages> localizer,
                                IUserService userService)
        {
            this.task_repository = task_repository;
            this.mapper = mapper;
            this.localizer = localizer;
            _userService = userService;
        }
        public async Task<ICollection<TaskDTO>> GetTasks(CustomerGetTasksRequest request)
        {
            var customerId = await _userService.GetCurrentUserId();

            Expression<Func<MyTask, bool>> filterCondition = e => e.CustomerId == customerId;

            if (request.TaskStateId != null)
            {
                filterCondition = filterCondition.And(e => e.StateId == request.TaskStateId);
            }

            var tasks = await task_repository.GetAsync(filter: filterCondition,
                                                       orderBy: e => e.OrderByDescending(e => e.StartTime),
                                                       includeProperties: $"{nameof(MyTask.State)}",
                                                       pageParameters: request.PageParameters);

            return mapper.Map<ICollection<TaskDTO>>(tasks);
        }

        public async Task<FullTaskDTO> GetFullTask(int id)
        {
            var task = (await task_repository.GetAsync(e => e.Id == id, includeProperties: $"{nameof(MyTask.State)},{nameof(MyTask.Customer)}")).FirstOrDefault();

            if (task == null) throw new HttpException(localizer[ErrorMessagePatterns.TaskNotFound], HttpStatusCode.NotFound);

            return mapper.Map<FullTaskDTO>(task);
        }
    }
}

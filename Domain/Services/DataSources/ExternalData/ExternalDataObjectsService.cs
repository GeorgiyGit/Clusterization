using Domain.DTOs.ExternalData;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.ExternalData;
using Domain.Entities.DataSources.Youtube;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DataSources.ExternalData;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Interfaces.Tasks;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Hangfire;
using Microsoft.Extensions.Localization;

namespace Domain.Services.DataSources.ExternalData
{
    public class ExternalDataObjectsService: IExternalDataObjectsService
    {
        private readonly IRepository<ClusterizationWorkspace> _workspacesRepository;
        private readonly IRepository<Comment> _commentsRepository;
        private readonly IRepository<MyDataObject> _dataObjectsRepository;
        private readonly IRepository<WorkspaceDataObjectsAddPack> _addPacksRepository;
        private readonly IRepository<ExternalObject> _externalObjectsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;
        private readonly IStringLocalizer<TaskTitles> _tasksLocalizer;

        private readonly IUserService _userService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMyTasksService _tasksService;
        private readonly IEmbeddingLoadingStatesService _embeddingLoadingStatesService;

        public ExternalDataObjectsService(IRepository<ClusterizationWorkspace> workspacesRepository,
            IRepository<Comment> commentsRepository,
            IRepository<MyDataObject> dataObjectsRepository,
            IRepository<WorkspaceDataObjectsAddPack> addPacksRepository,
            IRepository<ExternalObject> externalObjectsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IStringLocalizer<TaskTitles> tasksLocalizer,
            IUserService userService,
            IBackgroundJobClient backgroundJobClient,
            IMyTasksService tasksService,
            IEmbeddingLoadingStatesService embeddingLoadingStatesService)
        {
            _workspacesRepository = workspacesRepository;
            _commentsRepository = commentsRepository;
            _dataObjectsRepository = dataObjectsRepository;
            _addPacksRepository = addPacksRepository;
            _externalObjectsRepository = externalObjectsRepository;
            _localizer = localizer;
            _tasksLocalizer = tasksLocalizer;
            _userService = userService;
            _backgroundJobClient = backgroundJobClient;
            _tasksService = tasksService;
            _embeddingLoadingStatesService = embeddingLoadingStatesService;
        }

        public async Task LoadExternalData(AddExternalDataRequest data)
        {
            /*var workspace = (await _workspacesRepository.GetAsync(c => c.Id == data.WorkspaceId, includeProperties: $"{nameof(ClusterizationWorkspace.DataObjects)},{nameof(ClusterizationWorkspace.Type)}")).FirstOrDefault();

            var userId = await _userService.GetCurrentUserId();
            if (workspace == null || workspace.TypeId != ClusterizationTypes.External || (workspace.ChangingType == ChangingTypes.OnlyOwner && (userId == null || userId != workspace.OwnerId))) throw new HttpException(_localizer[ErrorMessagePatterns.WorkspaceNotFound], System.Net.HttpStatusCode.NotFound);

            var taskId = await _tasksService.CreateTask(_tasksLocalizer[TaskTitlesPatterns.AddingExternalDataToWorkspace]);
            float percent = 0f;

            await _tasksService.ChangeTaskState(taskId, TaskStates.Process);
            try
            {
                var file = data.File; // Assuming a single file is being uploaded

                if (file != null && file.Length > 0)
                {
                    var fileContent = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        fileContent = reader.ReadToEnd(); // Read the text content of the file
                    }
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalDataFileModelDTO>(fileContent);

                    if (responseObject == null) throw new HttpException(_localizer[ErrorMessagePatterns.FileError], HttpStatusCode.BadRequest);

                    if (oldExternalObjects.Count() > 0)
                    {
                        foreach (var newExtObj in responseObject.ExternalObjects)
                        {
                            var extObj = oldExternalObjects.FirstOrDefault(e => e.Id == newExtObj.Id);

                            if (extObj != null)
                            {
                                extObj.Text = newExtObj.Text;
                            }
                            else
                            {
                                var extObjectForAdding = new ExternalObject()
                                {
                                    Text = newExtObj.Text,
                                    Session = responseObject.Session,
                                    Id = newExtObj.Id,
                                    FullId = workspace.Id + responseObject.Session + newExtObj.Id
                                };
                                await externalObjects_repository.AddAsync(extObjectForAdding);

                                workspace.ExternalObjects.Add(extObjectForAdding);
                                extObjectForAdding.Workspaces.Add(workspace);

                                workspace.EntitiesCount++;
                            }
                        }
                    }
                    else
                    {
                        foreach (var newExtObj in responseObject.ExternalObjects)
                        {
                            var extObjectForAdding = new ExternalObject()
                            {
                                Text = newExtObj.Text,
                                Session = responseObject.Session,
                                Id = newExtObj.Id,
                                FullId = workspace.Id + responseObject.Session + newExtObj.Id
                            };
                            await _externalObjectsRepository.AddAsync(extObjectForAdding);

                            workspace.ExternalObjects.Add(extObjectForAdding);
                            extObjectForAdding.Workspaces.Add(workspace);

                            workspace.EntitiesCount++;
                        }
                    }

                    workspace.IsAllDataEmbedded = false;

                    await repository.SaveChangesAsync();

                    await taskService.ChangeTaskPercent(taskId, 100f);
                    await taskService.ChangeTaskState(taskId, TaskStates.Completed);
                }
                else
                {
                    await taskService.ChangeTaskState(taskId, TaskStates.Error);
                    await taskService.ChangeTaskDescription(taskId, "File is unavailable");
                }
            }
            catch (Exception ex)
            {
                await taskService.ChangeTaskState(taskId, TaskStates.Error);
                await taskService.ChangeTaskDescription(taskId, ex.Message);
            }
            */
        }
    }
}

using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests;
using Domain.DTOs.ExternalData;
using Domain.Entities.Clusterization.Workspaces;
using Domain.Entities.DataObjects;
using Domain.Entities.DataSources.ExternalData;
using Domain.Exceptions;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Localization.Tasks;
using Domain.Resources.Types.Clusterization;
using Domain.Resources.Types.DataSources;
using Domain.Resources.Types;
using Domain.Services.TaskServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses;
using TL.Methods;

namespace Domain.Interfaces.DataSources.ExternalData
{
    public interface IExternalDataObjectsPacksService
    {
        public Task LoadExternalDataObjectBackgroundJob(AddExternalDataRequest request);
        public Task AddExternalDataObjectsToWorkspace(AddExternalDataObjectsPacksToWorkspaceRequest request);
        public Task LoadingExternalDataAndAddingToWorkspace(AddExternalDataRequest request);

        public Task UpdatePack(UpdateExternalDataPackRequest request);

        public Task<ICollection<SimpleExternalObjectsPackDTO>> GetCollection(GetExternalDataObjectsPacksRequest request);
        public Task<FullExternalObjectsPackDTO> GetFullById(int id);
    }
}

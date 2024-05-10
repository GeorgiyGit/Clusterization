using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests
{
    public class AddExternalDataObjectsPacksToWorkspaceRequest
    {
        public int PackId { get; set; }
        public int WorkspaceId { get; set; }
    }
}

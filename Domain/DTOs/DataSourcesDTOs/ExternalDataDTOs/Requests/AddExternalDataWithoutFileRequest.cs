using Domain.DTOs.ExternalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests
{
    public class AddExternalDataWithoutFileRequest
    {
        public List<ExternalObjectModelDTO> ObjectsList { get; set; } = new List<ExternalObjectModelDTO>();
        public int? WorkspaceId { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

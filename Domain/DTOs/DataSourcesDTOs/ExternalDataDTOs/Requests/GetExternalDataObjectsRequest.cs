using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests
{
    public class GetExternalDataObjectsRequest
    {
        public PageParameters PageParameters { get; set; }
        public int PackId { get; set; }
    }
}

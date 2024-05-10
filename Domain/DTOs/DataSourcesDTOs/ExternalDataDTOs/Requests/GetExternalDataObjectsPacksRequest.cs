using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests
{
    public class GetExternalDataObjectsPacksRequest
    {
        public PageParameters PageParameters { get; set; }
        public string FilterStr { get; set; } = "";
    }
}

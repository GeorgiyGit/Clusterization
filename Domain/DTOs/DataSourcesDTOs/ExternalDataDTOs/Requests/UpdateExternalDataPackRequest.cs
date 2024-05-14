using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Requests
{
    public class UpdateExternalDataPackRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
    }
}

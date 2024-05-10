using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses
{
    public class SimpleExternalObjectDTO
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.ExternalDataDTOs.Responses
{
    public class SimpleExternalObjectsPackDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
        public string OwnerId { get; set; }
    }
}

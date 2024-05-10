using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ExternalData
{
    public class AddExternalDataRequest
    {
        public IFormFile File { get; set; }
        public int? WorkspaceId { get; set; }

        public string VisibleType { get; set; }
        public string ChangingType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

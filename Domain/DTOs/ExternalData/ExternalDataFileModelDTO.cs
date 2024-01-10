using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ExternalData
{
    public class ExternalDataFileModelDTO
    {
        public string Session { get; set; }
        public ICollection<ExternalObjectModelDTO> ExternalObjects { get; set; } = new List<ExternalObjectModelDTO>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class LoadManyByIdsRequest
    {
        public ICollection<string> Ids { get; set; } = new List<string>();
    }
}

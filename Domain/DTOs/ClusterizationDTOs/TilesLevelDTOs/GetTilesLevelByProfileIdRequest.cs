using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.TilesLevelDTOs
{
    public class GetTilesLevelByProfileIdRequest
    {
        public int ProfileId { get; set; }
        public int X { get; set; }
    }
}

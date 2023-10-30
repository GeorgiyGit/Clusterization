using Domain.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.TileDTOs
{
    public class GetTileCollectionRequest
    {
        public int ProfileId { get; set; }
        public int Z { get; set; }
        public List<MyIntegerVector2> Points { get; set; } = new List<MyIntegerVector2>();
    }
}

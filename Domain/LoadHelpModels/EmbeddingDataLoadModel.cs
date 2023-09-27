using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LoadHelpModels
{
    public class EmbeddingDataLoadModel
    {
        public string Object { get; set; }
        public List<EmbeddingItemLoadModel> Data { get; set; }
        public string Model { get; set; }
        public EmbeddingUsageDataLoadModel Usage { get; set; }
    }
}

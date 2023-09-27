using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LoadHelpModels
{
    public class EmbeddingItemLoadModel
    {
        public string Object { get; set; }
        public int Index { get; set; }
        public List<double> Embedding { get; set; }
    }
}

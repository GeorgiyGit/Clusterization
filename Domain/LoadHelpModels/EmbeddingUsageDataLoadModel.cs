using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LoadHelpModels
{
    public class EmbeddingUsageDataLoadModel
    {
        public int PromptTokens { get; set; }
        public int TotalTokens { get; set; }
    }
}

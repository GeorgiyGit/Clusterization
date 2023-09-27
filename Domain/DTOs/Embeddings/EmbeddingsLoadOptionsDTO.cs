using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Embeddings
{
    public class EmbeddingsLoadOptionsDTO
    {
        public string? VideoId { get; set; }
        public string? ChannelId { get; set; }
        public int MaxCount { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}

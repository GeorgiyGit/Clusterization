using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class LoadOptions
    {
        public string? ParentId { get; set; }
        public DateTime? DateFrom { get; set; } //Load from that date
        public DateTime? DateTo { get; set; } = DateTime.UtcNow;//Load to that date
    
        public int MaxLoad { get; set; }

        public ulong? MinCommentCount { get; set; }
        public ulong? MinViewCount { get; set; }
    }
}

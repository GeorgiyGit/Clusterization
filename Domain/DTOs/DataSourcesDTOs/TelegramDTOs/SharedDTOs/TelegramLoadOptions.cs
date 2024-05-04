using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs
{
    public class TelegramLoadOptions
    {
        public long? ParentId { get; set; }
        public DateTime? DateTo { get; set; } //Load to that date

        public int MaxLoad { get; set; }
    }
}

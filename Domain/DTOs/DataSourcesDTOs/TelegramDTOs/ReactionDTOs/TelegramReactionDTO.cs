using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.ReactionDTOs
{
    public class TelegramReactionDTO
    {
        public long Id { get; set; }
        public int Count { get; set; }

        public string? Emotion { get; set; }
        public long? DocumentId { get; set; }

        public bool IsCustom { get; set; }
    }
}

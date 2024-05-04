using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DataSourcesDTOs.TelegramDTOs.SharedDTOs
{
    public class AddTelegramRepliesToWorkspaceByMessagesRequest
    {
        public int WorkspaceId { get; set; }

        public int MaxCountInVideo { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public ICollection<long> MessageIds { get; set; } = new List<long>();
    }
}

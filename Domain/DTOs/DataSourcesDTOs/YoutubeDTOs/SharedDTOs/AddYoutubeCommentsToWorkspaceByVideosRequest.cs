using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ClusterizationDTOs.WorkspaceDTOs.RequestDTOs
{
    public class AddYoutubeCommentsToWorkspaceByVideosRequest
    {
        public int WorkspaceId { get; set; }

        public int MaxCountInVideo { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public ICollection<string> VideoIds { get; set; } = new List<string>();
    }
}

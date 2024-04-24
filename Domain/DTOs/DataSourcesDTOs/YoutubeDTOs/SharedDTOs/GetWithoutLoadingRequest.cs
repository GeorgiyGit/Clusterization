using Domain.Resources.Types;
using Domain.Resources.Types.DataSources.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.YoutubeDTOs.Requests
{
    public class GetWithoutLoadingRequest
    {
        public string Name { get; set; }
        public string? NextPageToken { get; set; }
        public string? ChannelId { get; set; }

        public string FilterType { get; set; } = LoadFilterOptions.Rating;
    }
}

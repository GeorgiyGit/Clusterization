using Domain.Interfaces.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Clusterization.Hubs
{
    public class MyHubHelper : IMyHubHelper
    {
        private readonly IHubContext<MySignalRHub> _hubContext;
        public MyHubHelper(IHubContext<MySignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Send(string groupName, object arg)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("Receive", arg);
        }
    }
}

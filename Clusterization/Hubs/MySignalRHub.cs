using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Clusterization.Hubs
{
    public class MySignalRHub : Hub
    {
        public async Task Enter(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task Leave(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}

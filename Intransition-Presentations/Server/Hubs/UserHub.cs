using Microsoft.AspNetCore.SignalR;

namespace Itransition.Server.Hubs
{
    public class UserHub : Hub
    {
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
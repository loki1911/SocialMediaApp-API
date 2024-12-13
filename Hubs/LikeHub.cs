using Microsoft.AspNetCore.SignalR;
using MychatAPI.Models;

namespace MychatAPI.Hubs
{
    public class LikeHub : Hub
    {
        public async Task SendLike(Like like)
        {
            await Clients.All.SendAsync("ReceiveLike", like); 
        }
    }
}

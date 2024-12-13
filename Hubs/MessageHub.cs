using Microsoft.AspNetCore.SignalR;
using MychatAPI.Data;
using MychatAPI.Models;

namespace MychatAPI.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public MessageHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

       
            public async Task SendMessage(Message message)
            {
                await Clients.User(message.ReceiverId.ToString()).SendAsync("ReceiveMessage", message);
            }
        
    }
}

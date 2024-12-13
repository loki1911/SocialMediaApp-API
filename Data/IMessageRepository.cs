using MychatAPI.Models;

namespace MychatAPI.Data
{
    public interface IMessageRepository
    {
      
            Task SendMessageAsync(Message message);
            Task<IEnumerable<Message>> GetMessagesAsync(string senderId, string receiverId);
        
    }
}

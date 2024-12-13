using Microsoft.AspNetCore.Mvc;
using MychatAPI.Data;
using MychatAPI.Models;

namespace MychatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            await _messageRepository.SendMessageAsync(message);
            return Ok();
        }

        [HttpGet("{senderId}/{receiverId}")]
        public async Task<IEnumerable<Message>> GetMessages(string senderId, string receiverId)
        {
            return await _messageRepository.GetMessagesAsync(senderId, receiverId);
        }
    }
}

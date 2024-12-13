using Microsoft.AspNetCore.Mvc;
using MychatAPI.Data;
using MychatAPI.Models;

namespace MychatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _likeRepository;

        public LikeController(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddLike([FromBody] Like like)
        {
            if (like == null)
                return BadRequest("Invalid like data.");

            var newLikeId = await _likeRepository.AddLikeAsync(like);
            return Ok(new { LikeId = newLikeId });
        }
    }
}

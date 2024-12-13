using Microsoft.AspNetCore.Mvc;
using MychatAPI.Data;
using MychatAPI.Models;

namespace MychatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostRepository _postRepository;

        public PostController(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromForm] PostRequest request)
        {
            if (request.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await request.ImageFile.CopyToAsync(memoryStream);
                var post = new Post
                {
                    userId = request.userId,
                    Content = request.Content,
                    Image = memoryStream.ToArray()
                };

                int newPostId = await _postRepository.CreatePostAsync(post);
                return Ok(new { PostId = newPostId });
            }

            return BadRequest("Image file is required");
        }

        [HttpGet("get-posts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPostsByUserId(int userId)
        {
            var posts = await _postRepository.GetPostsByUserIdAsync(userId);
            return Ok(posts);
        }
    }

    public class PostRequest
    {
        public int userId { get; set; }
        public string Content { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

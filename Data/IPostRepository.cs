using MychatAPI.Models;

namespace MychatAPI.Data
{
    public interface IPostRepository
    {
        Task<int> CreatePostAsync(Post post);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
    }
}

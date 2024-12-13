using MychatAPI.Models;

namespace MychatAPI.Data
{
    public interface ILikeRepository
    {
        Task<int> AddLikeAsync(Like like);
        Task<IEnumerable<Like>> GetLikesForPostAsync(int postId);
    }
}

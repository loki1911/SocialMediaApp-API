using MychatAPI.Models;
using System.Collections;

namespace MychatAPI.Data
{
    public interface IUserRepository
    {
         Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByUsernameAsync(string username);

        Task<User> GetUserByIdAsync(int userId);
        Task <int> CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string username);
    }
}

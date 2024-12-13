using Dapper;
using Microsoft.Data.SqlClient;
using MychatAPI.Models;
using System.Data;

namespace MychatAPI.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly string _connectionString;

      public PostRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> CreatePostAsync(Post post)
        {
            var sql = @"INSERT INTO Posts (userId, Content, Image) 
                        VALUES (@UserId, @Content, @Image);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = CreateConnection())
            {
                connection.Open();
                return await connection.ExecuteScalarAsync<int>(sql, new
                {
                    post.userId,
                    post.Content,
                    post.Image
                });
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var sql = "SELECT * FROM Posts";
            using (var connection = CreateConnection())
            {
                connection.Open();
                return await connection.QueryAsync<Post>(sql);
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Posts WHERE UserId = @UserId";
                return await connection.QueryAsync<Post>(sql, new { UserId = userId });
            }
        }
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using MychatAPI.Models;

namespace MychatAPI.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly string _connectionString;

        public LikeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddLikeAsync(Like like)
        {
            var sql = @"INSERT INTO Likes (PostId, UserId,username) 
                        VALUES (@PostId, @UserId, @username);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<int>(sql, like);
            }
        }

        public async Task<IEnumerable<Like>> GetLikesForPostAsync(int postId)
        {
            var sql = "SELECT * FROM Likes WHERE PostId = @PostId";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Like>(sql, new { PostId = postId });
            }
        }
    }
}


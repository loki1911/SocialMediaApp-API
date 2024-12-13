using Dapper;
using Microsoft.Data.SqlClient;
using MychatAPI.Models;
using System.Data;

namespace MychatAPI.Data
{
    public class UserRepository : IUserRepository  
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var sql = "GetAllUsers"; 
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return await connection.QueryAsync<User>(sql, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users from database.", ex);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                var sql = "SELECT * FROM Users WHERE username = @Username";
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user '{username}' from database.", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                var sql = "SELECT * FROM Users WHERE userId = @userId";
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return await connection.QuerySingleOrDefaultAsync<User>(sql, new { userId = userId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user '{userId}' from database.", ex);
            }
        }



        public async Task<int> CreateUserAsync(User user)
        {
            try
            {
                var sql = @"INSERT INTO Users (username, password, email, fullName) 
                            VALUES (@Username, @Password, @Email, @FullName);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        user.username,
                        user.password,
                        user.email,
                        user.fullName
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating new user in database.", ex);
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            try
            {
                var sql = "SELECT COUNT(1) FROM Users WHERE username = @Username";
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<bool>(sql, new { Username = username });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if user '{username}' exists in database.", ex);
            }
        }

      
    }
}

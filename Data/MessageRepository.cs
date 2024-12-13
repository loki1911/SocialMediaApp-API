using Dapper;
using Microsoft.Data.SqlClient;
using MychatAPI.Models;
using System.Data;


namespace MychatAPI.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;

        public MessageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task SendMessageAsync(Message message)
        {
            var sql = @"INSERT INTO Messages (SenderId, ReceiverId, MessageContent) 
                        VALUES (@SenderId, @ReceiverId, @MessageContent)";

            using (var connection = CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new
                {
                    message.SenderId,
                    message.ReceiverId,
                    message.MessageContent,
                });
            }
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string senderId, string receiverId)
        {
            var sql = @"SELECT * FROM Messages 
                        WHERE (SenderId = @SenderId AND ReceiverId = @ReceiverId) ";
                        

            using (var connection = CreateConnection())
            {
                connection.Open();
                return await connection.QueryAsync<Message>(sql, new { SenderId = senderId, ReceiverId = receiverId });
            }
        }
    }
}
    
 
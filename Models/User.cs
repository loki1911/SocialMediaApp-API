using System.ComponentModel.DataAnnotations;

namespace MychatAPI.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        public string username { get; set; }


        public string password { get; set; }


        public string? email { get; set; } = string.Empty;

        public string? fullName { get; set; } = string.Empty;

    }
}

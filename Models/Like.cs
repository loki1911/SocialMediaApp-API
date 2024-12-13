using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MychatAPI.Models
{
    public class Like
    {

        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? userName { get; set; } = string.Empty;

       
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MychatAPI.Models
{
    public class Post
    {
        [ForeignKey("UserId")]
            public int userId { get; set; }
            public string Content { get; set; }
            public byte[] Image { get; set; }
        
    }
}

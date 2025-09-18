using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flappy_Bird_API.Models
{
    [Table("score", Schema = "public")]
    public class Score
    {
        [Key]
        public int ID { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

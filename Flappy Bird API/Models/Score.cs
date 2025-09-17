using System.ComponentModel.DataAnnotations.Schema;

namespace Flappy_Bird_API.Models
{
    [Table("SCORE")]
    public class Score
    {
        public int ID { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

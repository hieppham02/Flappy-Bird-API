using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flappy_Bird_API.Models
{
    [Table("score", Schema = "public")]
    public class Score
    {
        [Key]
        public int id { get; set; }
        public string playername { get; set; } = string.Empty;
        public int points { get; set; }
        public string createdat { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

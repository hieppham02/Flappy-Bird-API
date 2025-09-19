using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flappy_Bird_API.Models
{
    [Table("score", Schema = "public")]
    public class Score
    {
        [Key]
        [Required(ErrorMessage = "Player name is required.")]

        public string playername { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Points must be a non-negative integer.")]

        public int points { get; set; }

        public string mode { get; set; }

        public string createdat { get; set; }
    }
}

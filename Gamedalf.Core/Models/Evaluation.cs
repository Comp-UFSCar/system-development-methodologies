using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }

        public string Review { get; set; }

        [Range(0, 5)]
        public short Score { get; set; }

        [ForeignKey("Game")]
        [Index("IX_GameAndPlayer", 1, IsUnique = true)]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [ForeignKey("Player")]
        [Index("IX_GameAndPlayer", 2, IsUnique = true)]
        public string PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}

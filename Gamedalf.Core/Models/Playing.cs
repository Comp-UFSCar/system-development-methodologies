using Gamedalf.Core.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    public class Playing : IDateTrackable
    {
        [Key]
        public int Id { get; set; }

        public ulong TimePlayed { get; set; }

        #region Review

        public string Review { get; set; }

        [Range(0, 5)]
        public short? Score { get; set; }

        public DateTime? ReviewDateCreated { get; set; }

        public bool IsEvaluated { get { return ReviewDateCreated != null; } }

        #endregion

        [ForeignKey("Game")]
        [Index("IX_GameAndPlayer", 1, IsUnique = true)]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [ForeignKey("Player")]
        [Index("IX_GameAndPlayer", 2, IsUnique = true)]
        public string PlayerId { get; set; }
        public virtual Player Player { get; set; }

        #region IDateTrackable
        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}

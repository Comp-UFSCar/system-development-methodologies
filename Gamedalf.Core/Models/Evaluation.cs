using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Gamedalf.Core.Models
{
    public class Evaluation
    {
        [Key]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Key]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Range(0, 5)]
        short Score;

        [MinLength(4)]
        [MaxLength(500)]
        string Review;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Gamedalf.Core.Models
{
    class Playing
    {
        [Key]
        public string PlayerId { get; set; }

        public virtual Player Player { get; set; }

        [Key]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public decimal MoneyExpended { get; set; }
        public TimeSpan TimePlayed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    [Table("Developers")]
    public class Developer : Player
    {
        public DateTime DateConverted { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}

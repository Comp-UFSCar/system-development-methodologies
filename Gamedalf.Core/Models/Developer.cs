using System;
using System.Collections.Generic;

namespace Gamedalf.Core.Models
{
    public class Developer : Player
    {
        public DateTime DateConverted { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}

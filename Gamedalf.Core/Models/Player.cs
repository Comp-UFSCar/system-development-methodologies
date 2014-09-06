using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamedalf.Core.Models
{
    class Player : ApplicationUser
    {
        public virtual ICollection<Evaluation> Evaluations { get; set; }

        public virtual ICollection<Playing> Games { get; set; }
    }
}

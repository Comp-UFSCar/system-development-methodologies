using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamedalf.Core.Models
{
    public class Employee : ApplicationUser
    {
        public string SSN { get; set; }

        public virtual ICollection<Game> ValidatedGames { get; set; }
    }
}

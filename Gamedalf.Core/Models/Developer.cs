using Gamedalf.Core.Infrastructure;
using System.Collections.Generic;

namespace Gamedalf.Core.Models
{
    public class Developer : Player
    {
        public virtual ICollection<Game> Games { get; set; }
    }
}

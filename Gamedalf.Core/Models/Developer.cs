using Gamedalf.Core.Infrastructure;

namespace Gamedalf.Core.Models
{
    public class Developer : Player
    {
        public virtual ICollection<Game> Games { get; set; }
    }
}

using Gamedalf.Core.Models;
using System.Collections.Generic;

namespace Gamedalf.ViewModels
{
    public class HomeViewModel
    {
        public virtual ICollection<Game> Games { get; set; }
    }
}
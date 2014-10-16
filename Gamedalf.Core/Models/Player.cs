using Gamedalf.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.Core.Models
{
    public class Player : ApplicationUser
    {
        [Display(Name = "Birth date")]
        public DateTime? DateBirth { get; set; }

        public virtual ICollection<Playing> Playing { get; set; }
    }
}

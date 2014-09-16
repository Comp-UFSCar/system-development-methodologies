using Gamedalf.Core.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.Core.Models
{
    public class Player : ApplicationUser, IDateTrackable
    {
        [Display(Name = "Birth date")]
        public DateTime? DateBirth { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DateUpdated { get; set; }
    }
}

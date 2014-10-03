using Gamedalf.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    public class Employee : ApplicationUser, IDateTrackable
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        // [Index(IsUnique = true)]
        public string SSN { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}

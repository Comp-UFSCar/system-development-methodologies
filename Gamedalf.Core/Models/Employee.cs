using Gamedalf.Core.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    public class Employee : ApplicationUser, IDateTrackable
    {
        [Required]
        public string SSN { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DateUpdated { get; set; }
    }
}

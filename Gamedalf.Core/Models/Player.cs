using Gamedalf.Core.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.Core.Models
{
    class Player : ApplicationUser, IDateTrackable
    {
        [Required]
        public DateTime DateBirth { get; set; }
    }
}

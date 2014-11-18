using Gamedalf.Core.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.Core.Models
{
    public class Subscription : IDateCreatedTrackable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Cost { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }
    }
}

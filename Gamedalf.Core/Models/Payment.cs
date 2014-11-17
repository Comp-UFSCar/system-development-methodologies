using Gamedalf.Core.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    public class Payment : IDateCreatedTrackable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Player")]
        public string PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public DateTime DateCreated { get; set; }

        public decimal value { get; set; }
    }
}

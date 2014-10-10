using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gamedalf.Core.Infrastructure;

namespace Gamedalf.Core.Models
{
    public class Game : IDateTrackable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        [ScaffoldColumn(false)]
        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ScaffoldColumn(false)]
        [ForeignKey("Developer")]
        public string DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }

        public virtual ICollection<Playing> Playings { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DateUpdated { get; set; }
    }
}

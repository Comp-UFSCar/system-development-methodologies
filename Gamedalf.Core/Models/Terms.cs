using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gamedalf.Core.Infrastructure;

namespace Gamedalf.Core.Models
{
    public class Terms : IDateCreatedTrackable
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        public string Title { get; set; }

        [ScaffoldColumn(false)]
        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Required]
        public string Content { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }
    }
}

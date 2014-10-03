using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
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
    }
}

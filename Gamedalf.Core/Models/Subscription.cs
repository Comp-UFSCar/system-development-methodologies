using System.ComponentModel.DataAnnotations;

namespace Gamedalf.Core.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Cost { get; set; }
    }
}

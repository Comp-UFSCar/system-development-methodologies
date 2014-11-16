using Gamedalf.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamedalf.Core.Models
{
    public class Payment : IDateCreatedTrackable
    {
        [Key]
        public int Id{get; set;}
        [ForeignKey("Player")]
        public string PlayerId;
        
        public DateTime DateCreated { get; set;}
        public virtual Player Player { get; set; }
        
        public Payment(){
            DateCreated = DateTime.Now;
        }
    }
}

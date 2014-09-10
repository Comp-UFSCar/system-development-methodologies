
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gamedalf.Core.Models
{
    public class Employee : ApplicationUser
    {
        public string SSN { get; set; }
    }
}

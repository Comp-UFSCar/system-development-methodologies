using System.ComponentModel.DataAnnotations;

namespace Gamedalf.ViewModels
{
    public class DeveloperRegisterViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public bool AcceptTerms { get; set; }
    }
}

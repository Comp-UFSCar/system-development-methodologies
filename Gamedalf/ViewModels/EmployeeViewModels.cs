using Gamedalf.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.ViewModels
{
    public class EmployeeEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Social security number")]
        [StringLength(20, MinimumLength=5)]
        public string SSN { get; set; }
    }

    public class EmployeeRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Social security number")]
        [StringLength(20, MinimumLength = 5)]
        public string SSN { get; set; }
    }
}

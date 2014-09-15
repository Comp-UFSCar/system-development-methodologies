using Gamedalf.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.ViewModels
{
    public class PlayerEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        public DateTime DateBirth { get; set; }
    }

    public class PlayerRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Birth date")]
        public DateTime DateBirth { get; set; }
    }
}

using Gamedalf.Core.Attributes;
using Gamedalf.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gamedalf.ViewModels
{
    public class TermsCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class TermsEditViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class AcceptTermsViewModel
    {
        [EnforceTrue]
        [Display(Name = "I hereby accept the presented terms")]
        public bool AcceptTerms { get; set; }

        public Terms Terms { get; set; }
    }

    public class TermsJsonViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

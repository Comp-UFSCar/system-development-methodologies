using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gamedalf.Core.Attributes;

namespace Gamedalf.ViewModels
{
    public class DeveloperAcceptTermsViewModel
    {
        [EnforceTrue]
        [Display(Name = "I hereby accept the presented terms")]
        public bool AcceptTerms { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Gamedalf.Core.Attributes;

namespace Gamedalf.ViewModels
{
    public class DeveloperRegisterViewModel
    {
        [EnforceTrue]
        public bool AcceptTerms { get; set; }
    }
}

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

    public class TermsGroupViewModel
    {
        //
    }
}

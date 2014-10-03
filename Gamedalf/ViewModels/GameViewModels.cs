using System.ComponentModel.DataAnnotations;

namespace Gamedalf.ViewModels
{
    public class GameCreateViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }
    }

    public class GameEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}

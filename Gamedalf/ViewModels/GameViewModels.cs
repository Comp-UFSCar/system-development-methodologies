using Gamedalf.Core.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Gamedalf.ViewModels
{
    public class GameCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Suggested price")]
        public decimal Price { get; set; }
    }

    public class GameEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class GameDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Players { get; set; }
        public bool DoIPlayIt { get; set; }
    }

    public class GameImagesViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Override all previous pictures")]
        public bool Reset { get; set; }

        [FileSize(2097152)]
        [FileTypes("jpg")]
        [Display(Name = "Cover picture")]
        public HttpPostedFileBase Cover { get; set; }

        [FilesSize(2097152)]
        [FilesTypes("jpg")]
        [Display(Name = "Art gallery pictures")]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gamedalf.ViewModels
{
    public class EvaluationViewModel
    {
        public int Id { get; set; }

        public string PlayerEmail { get; set; }
        public string GameTitle { get; set; }

        public string Review { get; set; }

        [Required(ErrorMessage = "You must define a score for this game")]
        public short Score { get; set; }
    }
}
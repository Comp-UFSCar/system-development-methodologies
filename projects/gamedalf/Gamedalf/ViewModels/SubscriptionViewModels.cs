using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gamedalf.ViewModels
{
    public class SubscriptionCreateViewModel
    {
        [Required]
        [Display(Name = "Subscription cost")]
        public decimal Cost { get; set; }
    }
}
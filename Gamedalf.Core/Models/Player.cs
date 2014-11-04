using Gamedalf.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamedalf.Core.Models
{
    [Table("Players")]
    public class Player : ApplicationUser
    {
        [Display(Name = "Birth date")]
        public DateTime? DateBirth { get; set; }

        public virtual ICollection<Playing> Playing { get; set; }

        /// <summary>
        /// Date this player accepted the power and responsabilities of a developer
        /// </summary>
        public DateTime? DateConverted { get; set; }

        /// <summary>
        /// Games registered by this developer
        /// </summary>
        public virtual ICollection<Game> GamesRegistered { get; set; }
    }
}

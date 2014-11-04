
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
namespace Gamedalf.Core.Migrations
{
    class TermsSeedData
    {
        public ICollection<Terms> Data { get; set; }

        public TermsSeedData()
        {
            Data = new List<Terms>
            {
                new Terms
                {
                    Title = "Developers",
                }
            };
        }

    }
}

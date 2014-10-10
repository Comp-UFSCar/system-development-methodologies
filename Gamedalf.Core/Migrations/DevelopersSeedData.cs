using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;

namespace Gamedalf.Core.Migrations
{
    class DevelopersSeedData
    {
        public ICollection<Developer> Data { get; set; }

        public DevelopersSeedData()
        {
            Data = new List<Developer>
            {
                new Developer
                {
                    Email     = "developer1@dev.com",
                    UserName  = "developer1@dev.com",
                    DateBirth     = DateTime.Today,
                    DateConverted = DateTime.Today,
                },
                new Developer
                {
                    Email     = "developer2@dev.com",
                    UserName  = "developer2@dev.com",
                    DateBirth     = DateTime.Today,
                    DateConverted = DateTime.Today,
                },
                new Developer
                {
                    Email     = "developer3@dev.com",
                    UserName  = "developer3@dev.com",
                    DateBirth     = DateTime.Today,
                    DateConverted = DateTime.Today,
                },
                new Developer
                {
                    Email     = "developer4@dev.com",
                    UserName  = "developer4@dev.com",
                    DateBirth     = DateTime.Today,
                    DateConverted = DateTime.Today,
                },
            };
        }
    }
}

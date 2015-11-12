using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System;
using System.Collections.Generic;

namespace Gamedalf.Tests.Testdata
{
    public class DevelopersTestData : ITestData<Player>
    {
        public ICollection<Player> Data { get; set; }

        public DevelopersTestData()
        {
            Data = new List<Player>
            {
                new Player 
                {
                    Id       = "developer1",
                    Email    = "developer1@test.com",
                    UserName = "developer1@test.com",
                    DateConverted = DateTime.Now,
                },
                new Player
                {
                    Id       = "developer2",
                    Email    = "developer2@test.com",
                    UserName = "developer2@test.com",
                    DateConverted = DateTime.Now,
                },
                new Player
                {
                    Id       = "developer3",
                    Email    = "developer3@test.com",
                    UserName = "developer3@test.com",
                    DateConverted = DateTime.Now,
                }
            };
        }
    }
}

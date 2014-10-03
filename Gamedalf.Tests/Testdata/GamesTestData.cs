using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System.Collections.Generic;

namespace Gamedalf.Tests.Testdata
{
    public class GamesTestData : ITestData<Game>
    {
        public ICollection<Game> Data { get; set; }

        public GamesTestData()
        {
            Data = new List<Game>
            {
                new Game 
                {
                    Id = 1,
                    Price = 1,
                    Title = "Minecraft",
                    DeveloperId = "developer1"
                },
                new Game
                {
                    Id = 2,
                    Price = 2,
                    Title = "Warcraft",
                    DeveloperId = "developer2"
                },
                new Game
                {
                    Id = 3,
                    Price = 2,
                    Title = "Assassin's Creed",
                    DeveloperId = "developer3"
                },
            };
        }
    }
}


using Gamedalf.Core.Models;
using System.Collections.Generic;
namespace Gamedalf.Core.Migrations
{
    class GamesSeedData
    {
        public ICollection<Game> Data { get; set; }

        public GamesSeedData()
        {
            Data = new List<Game>
            {
                new Game
                {
                    Id          = 1,
                    Title       = "Minecraft",
                    Description = "A game where you can lose your life.",
                    Price       = 1
                },
                new Game
                {
                    Id          = 2,
                    Title       = "The Hive Queen",
                    Description = "And the Hegemon",
                    Price       = 1
                },
                new Game
                {
                    Id          = 3,
                    Title       = "Mass Effect",
                    Description = "Shepard commander",
                    Price       = 2
                },
                new Game
                {
                    Id          = 4,
                    Title       = "Chef",
                    Description = "",
                    Price       = 1
                },
                new Game
                {
                    Id          = 5,
                    Title       = "Kirby",
                    Description = "Pinky anoyence",
                    Price       = 1
                },
                new Game
                {
                    Id          = 6,
                    Title       = "Tower defence",
                    Description = "Another copy of Warcraft.",
                    Price       = 1
                },
            };
        }
    }
}

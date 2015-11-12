using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System.Collections.Generic;

namespace Gamedalf.Tests.Testdata
{
    public class PlayersTestData : ITestData<Player>
    {
        public ICollection<Player> Data { get; set; }

        public PlayersTestData()
        {
            Data = new List<Player>
            {
                new Player 
                {
                    Id       = "player1",
                    Email    = "player1@test.com",
                    UserName = "player1@test.com",
                },
                new Player
                {
                    Id       = "player2",
                    Email    = "player2@test.com",
                    UserName = "player2@test.com",
                },
                new Player
                {
                    Id       = "player3",
                    Email    = "player3@test.com",
                    UserName = "player3@test.com"
                }
            };
        }
    }
}

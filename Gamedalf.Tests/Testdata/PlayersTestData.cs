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
                    Email    = "lucasolivdavid@gmail.com",
                    UserName = "lucasolivdavid@gmail.com",
                },
                new Player
                {
                    Id       = "player2",
                    Email    = "maria@db.net",
                    UserName = "maria@db.net",
                },
                new Player
                {
                    Id       = "player3",
                    Email    = "johnhall@provider.com",
                    UserName = "johnhall@provider.com"
                },
                new Player
                {
                    Id       = "player4",
                    Email    = "email@mail.com",
                    UserName = "email@mail.com"
                }
            };
        }
    }
}

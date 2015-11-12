using System;
using Gamedalf.Core.Models;
using Gamedalf.Tests.Infrastructure;
using System.Collections.Generic;
using Gamedalf.Tests.Infrastructure.Exceptions;

namespace Gamedalf.Tests.Testdata
{
    public class PlayingTestData : ITestData<Playing>
    {
        public ICollection<Playing> Data { get; set; }

        public PlayingTestData()
        {
            try
            {
                var players = (List<Player>) new PlayersTestData().Data;
                var games = (List<Game>) new GamesTestData().Data;

                Data = new List<Playing>
                {
                    new Playing
                    {
                        Id = 1,
                        GameId = games[0].Id,
                        Game   = games[0],
                        PlayerId = players[0].Id,
                        Player   = players[0],
                        DateCreated = DateTime.Now,
                        ReviewDateCreated = DateTime.Now,
                        Score = 5
                    },
                    new Playing
                    {
                        Id = 2,
                        GameId = games[1].Id,
                        Game   = games[1],
                        PlayerId = players[1].Id,
                        Player   = players[1],
                        DateCreated = DateTime.Now
                    },
                    new Playing
                    {
                        Id = 3,
                        GameId = games[2].Id,
                        Game   = games[2],
                        PlayerId = players[2].Id,
                        Player   = players[2],
                        DateCreated = DateTime.Now
                    },
                };
            }
            catch (Exception)
            {
                throw new TestDataInitializationException();
            }
        }
    }
}

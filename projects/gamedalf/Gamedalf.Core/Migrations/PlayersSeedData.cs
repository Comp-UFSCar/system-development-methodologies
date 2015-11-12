using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;

namespace Gamedalf.Core.Migrations
{
    class PlayersSeedData
    {
        public ICollection<Player> Data { get; set; }

        public PlayersSeedData()
        {
            Data = new List<Player>
            {
                new Player
                {
                    Email     = "gollen@g4.edu",
                    UserName  = "gollen@g4.edu",
                    DateBirth = DateTime.Today,
                    DateCreated = DateTime.Now
                },
                new Player
                {
                    Email     = "player@rmail.com",
                    UserName  = "player@rmail.com",
                    DateBirth = DateTime.Today,
                    DateCreated = DateTime.Now,
                },
                new Player
                {
                    Email     = "yolanda@y.com",
                    UserName  = "yolanda@y.com",
                    DateBirth = DateTime.Today,
                    DateCreated = DateTime.Now,
                },
                new Player
                {
                    Email     = "flin@webmail.net",
                    UserName  = "flin@webmail.net",
                    DateBirth = DateTime.Today,
                    DateCreated = DateTime.Now,
                },
            };
        }
    }
}

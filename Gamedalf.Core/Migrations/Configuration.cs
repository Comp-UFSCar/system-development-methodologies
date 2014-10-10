using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Gamedalf.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Gamedalf.Core.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Gamedalf.Core.Models;
    using System.Collections.Generic;
    using System.Data.Common;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roles = SeedRoles(context);
            var employees = SeedEmployees(context);
            var players = SeedPlayers(context);
            var developers = SeedDevelopers(context);
            var games = SeedGames(context, developers);
            var playings = SeedPlayings(context, players, games);
        }

        private ICollection<IdentityRole> SeedRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("admin"))
            {
                roleManager.Create(new IdentityRole("admin"));
            }

            if (!roleManager.RoleExists("employee"))
            {
                roleManager.Create(new IdentityRole("employee"));
            }

            if (!roleManager.RoleExists("player"))
            {
                roleManager.Create(new IdentityRole("player"));
            }

            if (!roleManager.RoleExists("developer"))
            {
                roleManager.Create(new IdentityRole("developer"));
            }

            return roleManager.Roles.Where(r => r.Id == "1").ToList();
        }

        private ICollection<Employee> SeedEmployees(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            // retrieve employee initial data
            var data = new EmployeesSeedData().Data;

            foreach (Employee employee in data)
            {
                var result = manager.Create(employee, "password");

                if (result.Succeeded)
                {
                    manager.AddToRole(employee.Id, "employee");
                    manager.AddToRole(employee.Id, "admin");
                }
            }

            return data;
        }

        private ICollection<Player> SeedPlayers(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            // retrieve employee initial data
            var data = new PlayersSeedData().Data;

            foreach (Player player in data)
            {
                var result = manager.Create(player, "password");

                if (result.Succeeded)
                {
                    manager.AddToRole(player.Id, "player");
                }
            }

            return data;
        }

        private ICollection<Developer> SeedDevelopers(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            // retrieve employee initial data
            var data = new DevelopersSeedData().Data;

            foreach (Player developer in data)
            {
                var result = manager.Create(developer, "password");

                if (result.Succeeded)
                {
                    manager.AddToRole(developer.Id, "developer");
                }
            }

            return data;
        }

        private ICollection<Game> SeedGames(ApplicationDbContext context, ICollection<Developer> developers)
        {
            var data = new GamesSeedData().Data;

            foreach (var game in data)
            {
                game.DeveloperId = developers.First().Id;
            }

            var result = context.Games.AddRange(data);

            return data;
        }

        private object SeedPlayings(ApplicationDbContext context, ICollection<Player> players, ICollection<Game> games)
        {
            var random   = new Random();
            var playings = new List<Playing>();

            foreach (var player in players)
            {
                foreach (var game in games)
                {
                    ulong timePlayed = (ulong)random.Next(100);
                    playings.Add(new Playing
                    {
                        PlayerId = player.Id,
                        GameId = game.Id,
                        TimePlayed = timePlayed,
                        DateCreated = DateTime.Today
                    });
                }
            }

            context.Playings.AddRange(playings);

            return playings;
        }
    }
}

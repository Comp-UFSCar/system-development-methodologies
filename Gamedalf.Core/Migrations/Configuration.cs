
namespace Gamedalf.Core.Migrations
{
    using Gamedalf.Core.Data;
    using Gamedalf.Core.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //var roles = SeedRoles(context);
            //var employees = SeedEmployees(context);
            //var players = SeedPlayers(context);
            //var developers = SeedDevelopers(context);
            //var games = SeedGames(context, developers);
            //var playings = SeedPlayings(context, players, games);
            var payments = SeedPayments(context, context.Players.ToList());
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

        private ICollection<Player> SeedDevelopers(ApplicationDbContext context)
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
                    manager.AddToRole(developer.Id, "player");
                    manager.AddToRole(developer.Id, "developer");
                }
            }

            return data;
        }

        private ICollection<Game> SeedGames(ApplicationDbContext context, ICollection<Player> developers)
        {
            var data = new GamesSeedData().Data;

            foreach (var game in data)
            {
                game.DeveloperId = developers.First().Id;

                if (!context.Games.Where(g => g.Title == game.Title).Any())
                {
                    context.Games.Add(game);
                }
            }

            return data;
        }

        private ICollection<Playing> SeedPlayings(ApplicationDbContext context, ICollection<Player> players, ICollection<Game> games)
        {
            var random = new Random();
            var playings = new List<Playing>();

            foreach (var player in players)
            {
                foreach (var game in games)
                {
                    ulong timePlayed = (ulong)random.Next(100);
                    var playing = new Playing
                    {
                        PlayerId = player.Id,
                        GameId = game.Id,
                        TimePlayed = timePlayed,
                        DateCreated = DateTime.Today
                    };

                    playings.Add(playing);

                    if (!context.Playings.Where(p => p.GameId == playing.GameId && p.PlayerId == playing.PlayerId).Any())
                    {
                        context.Playings.Add(playing);
                    }
                }
            }

            return playings;
        }

        private ICollection<Payment> SeedPayments(ApplicationDbContext context, ICollection<Player> players)
        {
            var payments = new PaymentSeedData().Data;

            try
            {
                context.Payments.RemoveRange(context.Payments.ToList());
                context.Payments.AddRange(payments);
            }
            catch (Exception e)
            {
                Console.Error.Write(e);
            }

            return payments;
        }
    }
}

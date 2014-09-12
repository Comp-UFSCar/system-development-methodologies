namespace Gamedalf.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Gamedalf.Core.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Gamedalf.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected async Task Seed(ApplicationDbContext context)
        {
            var roles = await SeedRoles(context);
        }

        private async Task<ICollection<IdentityRole>> SeedRoles(ApplicationDbContext context)
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

            return await roleManager.Roles.Where(r => r.Id == "1").ToListAsync();
        }
    }
}

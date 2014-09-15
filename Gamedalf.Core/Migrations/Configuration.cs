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

        protected override void Seed(ApplicationDbContext context)
        {
            var roles = SeedRoles(context);
            var employees = SeedEmployees(context);
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
                }
            }

            return data;
        }
    }
}

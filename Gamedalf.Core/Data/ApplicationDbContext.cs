using Gamedalf.Core.Infrastructure;
using Gamedalf.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDateTracker
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public void TrackDate()
        {
            foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified))
            {
                if (entity.State == EntityState.Added && entity.Entity is IDateCreatedTrackable)
                {
                    ((IDateCreatedTrackable)entity.Entity).DateCreated = DateTime.Now;
                }
                if (entity.State == EntityState.Modified && entity.Entity is IDateUpdatedTrackable)
                {
                    ((IDateUpdatedTrackable)entity.Entity).DateUpdated = DateTime.Now;
                }
            }
        }

        public override int SaveChanges()
        {
            TrackDate();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            TrackDate();
            return base.SaveChangesAsync();
        }

        public virtual DbSet<Employee>  Employees  { get; set; }
        public virtual DbSet<Player>    Players    { get; set; }
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Playing> Playings { get; set; }
        public virtual DbSet<Terms> Terms { get; set; }
    }
}

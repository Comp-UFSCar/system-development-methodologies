using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class DeveloperService : Service<Developer>
    {
        public DeveloperService(ApplicationDbContext db) : base(db) { }

        public virtual async Task<ICollection<Developer>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await db.Developers
                .Where(e => e.Email.Contains(q))
                .OrderBy(e => e.Email)
                .ToListAsync();
        }

        public virtual async Task<Developer> Convert(Player player)
        {
            var developer = new Developer
            {
                AccessFailedCount = player.AccessFailedCount,
                DateBirth = player.DateBirth,
                DateCreated = player.DateCreated,
                DateUpdated = player.DateUpdated,
                Email = player.Email,
                EmailConfirmed = player.EmailConfirmed,
                Id = player.Id,
                LockoutEnabled = player.LockoutEnabled,
                LockoutEndDateUtc = player.LockoutEndDateUtc,
                PasswordHash = player.PasswordHash,
                PhoneNumber = player.PhoneNumber,
                PhoneNumberConfirmed = player.PhoneNumberConfirmed,
                SecurityStamp = player.SecurityStamp,
                TwoFactorEnabled = player.TwoFactorEnabled,
                UserName = player.UserName
            };

            db.Players.Remove(player);
            await Add(developer);

            return developer;
        }
    }
}

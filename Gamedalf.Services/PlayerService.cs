using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class PlayerService : Service<Player>
    {
        public PlayerService(ApplicationDbContext db) : base(db) { }

        public virtual async Task<ICollection<Player>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await Db.Players
                .Where(e => e.Email.Contains(q))
                .OrderBy(e => e.Email)
                .ToListAsync();
        }

        public virtual async Task<Player> Convert(string id)
        {
            var developer = await Db.Players.FindAsync(id);
            developer.DateConverted = DateTime.Now;
            await Update(developer);
            return developer;
        }
    }
}

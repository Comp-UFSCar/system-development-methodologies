using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
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

            return await db.Players
                .Where(e => e.Email.Contains(q))
                .OrderBy(e => e.Email)
                .ToListAsync();
        }
    }
}

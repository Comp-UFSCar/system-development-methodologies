using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gamedalf.Services.Infrastructure;

namespace Gamedalf.Services
{
    public class GameService : Service<Game>
    {
        public GameService(ApplicationDbContext db) : base(db) { }

        public virtual async Task<ICollection<Game>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await Db.Games
                .Where(e => e.Title.Contains(q))
                .OrderBy(e => e.Title)
                .ToListAsync();
        }

        public async Task<ICollection<Game>> Recent(int amount)
        {
            return await Db.Games
                .OrderByDescending(g => g.DateCreated)
                .Take(amount)
                .ToListAsync();
        }
    }
}

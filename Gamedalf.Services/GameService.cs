using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class GameService : Service<Game>
    {
        public GameService(ApplicationDbContext _db) : base(_db) { }

        public virtual async Task<ICollection<Game>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await db.Games
                .Where(e => e.Title.Contains(q))
                .OrderBy(e => e.Title)
                .ToListAsync();
        }
    }
}

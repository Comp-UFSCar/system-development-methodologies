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

        public virtual async Task<ICollection<Game>> Recent(int amount)
        {
            return await Db.Games
                .OrderByDescending(g => g.DateCreated)
                .Take(amount)
                .ToListAsync();
        }

        public virtual async Task<ICollection<Game>> MostDownloaded(int amount)
        {
            var days = DateTime.Today.AddDays(-7);

            return await Db.Games
                .OrderByDescending(g =>
                    g.Playings.Where(p =>
                        p.DateCreated > days).Count())
                .Take(amount)
                .ToListAsync();
        }

        /// <summary>
        /// Returns a collection of games that were registered by the developer <paramref name="user"/> and 
        /// have their Title property containing the query <paramref name="q"/>.
        /// </summary>
        /// <param name="user">Identifier of the user which registered the games.</param>
        /// <param name="q">Query </param>
        /// <returns></returns>
        public virtual async Task<ICollection<Game>> RegisteredByUser(string user, string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await Db.Games
                    .Where(g => g.DeveloperId == user)
                    .ToListAsync();
            }

            return await Db.Games
                .Where(g => g.DeveloperId == user && g.Title.Contains(q))
                .OrderBy(g => g.Title)
                .ToListAsync();
        }

        /// <summary>
        /// Verifies if game exists in the database.
        /// </summary>
        /// <param name="id">Identifier of the game</param>
        /// <returns>True, if game exists. False otherwise.</returns>
        public virtual async Task<bool> Exists(int id)
        {
            return (await Db.Games.CountAsync(g => g.Id == id)) > 0;
        }
    }
}

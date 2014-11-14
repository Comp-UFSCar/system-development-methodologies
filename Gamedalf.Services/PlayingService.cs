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
    public class PlayingService : Service<Playing>
    {
        public PlayingService(ApplicationDbContext db) : base(db) { }

        public virtual async Task<Playing> Find(int id)
        {
            return await Db.Playings
                .Where(p => p.Id == id)
                .Include(p => p.Game).Include(p => p.Player)
                .FirstAsync();
        }

        public virtual async Task<ICollection<Playing>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await Db.Playings
                .Where(p =>
                       p.Game.Title.Contains(q)
                    || p.Player.Email.Contains(q)
                    || p.Game.Developer.Email.Contains(q)
                    || p.Game.Employee.Email.Contains(q))
                .Include(p => p.Game).Include(p => p.Player)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public virtual async Task<ICollection<Playing>> EvaluatedByUser(string id)
        {
            return await Db.Playings
                .Where(p =>
                    p.PlayerId == id && p.ReviewDateCreated != null)
                .ToListAsync();
        }

        public virtual async Task<ICollection<Playing>> NotYetEvaluatedByUser(string id)
        {
            return await Db.Playings
                .Where(p =>
                    p.PlayerId == id && p.ReviewDateCreated == null)
                .ToListAsync();
        }

        public virtual async Task<ICollection<Playing>> PlayingByUser(string id, string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await Db.Playings
                    .Where(p => p.PlayerId == id)
                    .Include(p => p.Player).Include(p => p.Game)
                    .ToListAsync();
            }

            return await Db.Playings
                .Where(p =>
                    p.PlayerId == id
                    && (p.Game.Title.Contains(q)
                    || p.Player.Email.Contains(q)
                    || p.Game.Developer.Email.Contains(q)
                    || p.Game.Employee.Email.Contains(q)))
                .Include(p => p.Player).Include(p => p.Game)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public virtual async Task<Playing> Evaluate(Playing evaluation)
        {
            var playing = await base.Find(evaluation.Id);
            
            playing.Review            = evaluation.Review;
            playing.Score             = evaluation.Score;
            playing.ReviewDateCreated = DateTime.Now;

            await  Update(playing);
            return playing;
        }
    }
}

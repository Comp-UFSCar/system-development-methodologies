using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class EvaluationService : Service<Evaluation>
    {
        public EvaluationService(ApplicationDbContext _db) : base(_db) { }

        public async Task<ICollection<Evaluation>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await db.Evaluations
                .Where(e => e.Player.Email.Contains(q)
                    || e.Game.Developer.Email.Contains(q)
                    || e.Game.Employee.Email.Contains(q))
                .OrderBy(e => e.Id)
                .ToListAsync();
        }
    }
}

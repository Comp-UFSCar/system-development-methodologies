using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services.Infrastructure;

namespace Gamedalf.Services
{
    public class TermsService : Service<Terms>
    {
        public TermsService(ApplicationDbContext db) : base(db) { }

        public virtual async Task<ICollection<Terms>> Search(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return await All();
            }

            return await Db.Terms
                .Where(t => t.Title.Contains(q) || t.Employee.Email.Contains(q))
                .Include(t => t.Employee)
                .OrderBy(e => e.Title)
                .ThenByDescending(e => e.DateCreated)
                .ToListAsync();
        }

        public virtual async Task<Terms> Latest(string q)
        {
            // Considering Search(q) has arranged them decreasingly, the first
            // element represents the one closest to the current date.
            return (await Search(q)).First();
        }

        public virtual async Task<Terms> TermsOnDate(string q, DateTime date)
        {
            // Gets the first of all terms that have their DateCreated < date.
            // Considering Search(q) has arranged them decreasingly, the first
            // element represents the one closest to date.
            return (await Search(q))
                .Where(t => t.DateCreated < date)
                .First();
        }
    }
}
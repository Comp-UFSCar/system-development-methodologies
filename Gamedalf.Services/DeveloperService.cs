using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gamedalf.Services.Infrastructure;
using System.Data.Entity.Validation;

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

            return await Db.Developers
                .Where(e => e.Email.Contains(q))
                .OrderBy(e => e.Email)
                .ToListAsync();
        }

        public virtual async Task<Developer> Convert(string id)
        {
            var developer = new Developer
            {
                Id = id,
                DateConverted = DateTime.Now
            };

            try {
                var result = await Update(developer);
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }

            return developer;
        }

        public async Task<Developer> Convert(Player player)
        {
            var developer = new Developer
            {
                Id = player.Id,
                UserName = player.UserName,
                DateCreated   = DateTime.Now,
                DateConverted = DateTime.Now
            };

            try
            {
                var result = await Update(developer);
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }

            return developer;
        }
    }
}

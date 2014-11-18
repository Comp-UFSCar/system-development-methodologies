using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using Gamedalf.Services.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public class SubscriptionService : Service<Subscription>
    {
        public SubscriptionService(ApplicationDbContext db) : base(db){}

        public virtual Subscription Latest()
        {
            return Db.Subscriptions.FirstOrDefault();
        }

        public async Task<ICollection<Subscription>> ReverseAll()
        {
            return await Db.Subscriptions
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }
    }
}

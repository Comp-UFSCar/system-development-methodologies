using Gamedalf.Core.Models;
using Gamedalf.Services.Infrastructure;
using System.Threading.Tasks;
using System.Linq;
using Gamedalf.Core.Data;

namespace Gamedalf.Services
{
    public class SubscriptionService : Service<Subscription>
    {
        public SubscriptionService(ApplicationDbContext db) : base(db){}

        public virtual Subscription Last()
        {
            return  Db.Subscriptions
                .Last();
        }
    }
}
